using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
#pragma warning disable IDE0051
#pragma warning disable IDE0052


public class BingoGameController : MonoBehaviour
{
    public GameObject bingoBoardPrefab;
    public Transform userBingoBoardContainer;
    public Transform aiBingoBoardContainer;

    public TMP_Text currentImageText;
    public Image currentImageDisplay;
    public Image bingoButtonCover;
    public Button bingoButton;
    public Button exitButton;

    public TMP_Text userResultText;
    public TMP_Text AIResultText;

    public TMP_Text timerText;
    public Image FillLoding;

    private float turnTimeLimit = 10f;
    private float timeRemaining;

    private List<ImageWordPair> imageWordPairs;
    private HashSet<int> usedImageIndices = new HashSet<int>();

    private ImageWordPair currentPair;

    private BingoBoardGenerator userBoardGenerator;
    private BingoBoardGenerator aiBoardGenerator;

    private BingoBoardData boardData;

    private bool isUserTurn = true;
    private int userBingoCount = 0;
    private int aiBingoCount = 0;
    private int coinsEarned;

    private string currentLevel;
    private string currentCategory;

    [SerializeField]
    private TMP_Text currentLevelText;

    private string currentLevelName;

    private Dictionary<string, string> levelNameMapping = new Dictionary<string, string>
    {
        { "Level1", "Zoom Lv1"},
        { "Level2", "Zoom Lv2"},
        { "Level3", "Zoom Lv3"},
        { "Level4", "Zoom Lv4"},
        { "Level5", "Keystone Lv1" },
        { "Level6", "Keystone Lv2" },
        { "Level7", "Keystone Lv3" },
        { "Level8", "Keystone Lv4" }
    };

    private int currentCoins = 0;

    private Dictionary<string, int> levelCoinMapping = new Dictionary<string, int>
    {
        { "Level1", 500 },
        { "Level2", 600 },
        { "Level3", 700 },
        { "Level4", 800 },
        { "Level5", 900 },
        { "Level6", 1000 },
        { "Level7", 1200 },
        { "Level8", 1500 }
    };

    void Start()
    {
        InitializeLevelName();

        UpdateCurrentLevelText();

        LoadGameData();

        string level = PlayerPrefs.GetString("SelectedLevel", "Unknown Level");

        timeRemaining = turnTimeLimit;
        FillLoding.fillAmount = 1;

        string boardDataJson = PlayerPrefs.GetString("SelectedBoardData", null);
        if ( string.IsNullOrEmpty(boardDataJson))
        {
            Debug.LogError("No board data found in PlayerPrefs");
            return;
        }
        
        boardData = JsonUtility.FromJson<BingoBoardData>(boardDataJson);
        if ( boardData == null || boardData.Pairs == null ||  boardData.Pairs.Count < 16 )
        {
            Debug.LogError("Invalid board data loaded");
            return;
        }

        InitializeGameBoard(boardData.Pairs);

        GameObject aiBoard = Instantiate(bingoBoardPrefab, aiBingoBoardContainer);
        aiBoardGenerator = aiBoard.GetComponent<BingoBoardGenerator>();
        aiBoardGenerator.PopulateBingoBoard(imageWordPairs, false);

        bingoButton.onClick.AddListener(OnBingoButtonClicked);
        bingoButton.interactable = false;

        exitButton.onClick.AddListener(OnExtiButtonClicked);
        StartCoroutine(GameLoop());
    }

    void InitializeLevelName()
    {
        string selectedLevel = PlayerPrefs.GetString("SelectedLevel", "Level1");

        if ( levelNameMapping.TryGetValue(selectedLevel, out string levelName))
        {
            currentLevelName = levelName;
        }
        else
        {
            currentLevelName = "Unknown Level";
        }
    }

    void UpdateCurrentLevelText()
    {
        if ( currentLevelText != null )
        {
            currentLevelText.text = $"{currentLevelName}";
        }
        else
        {
            Debug.LogWarning("CurrentLevelText is not assigned in the Inspector");
        }
    }

    void LoadGameData()
    {
        string level = PlayerPrefs.GetString("SelectedLevel", "Level1");
        int stage = PlayerPrefs.GetInt("SelectedStage", 1);

        string textPath = $"{level}Names-{stage}";
        imageWordPairs = BingoDataLoader.LoadBingoPairs(textPath);

        if ( imageWordPairs == null || imageWordPairs.Count == 0 )
        {
            Debug.LogError("Failed to load image-word pairs");
            return;
        }
    }

    void InitializeGameBoard(List<ImageWordPair> pairs)
    {
        if ( pairs == null || pairs.Count < 16)
        {
            Debug.LogError("Invalid pairs data for initializing the game board");
            return;
        }

        GameObject userBoard = Instantiate(bingoBoardPrefab, userBingoBoardContainer);
        userBoardGenerator = userBoard.GetComponent<BingoBoardGenerator>();
        userBoardGenerator.PopulateBingoBoard(pairs, true);
    }

    IEnumerator GameLoop()
    {
        while (true)
        {
            DisplayNextImage();

            // 사용자 턴 시작
            yield return UserTurn();

            // AI 턴 시작
            yield return AITurn();
            
            // 빙고 상태 확인 및 게임 종료 조건 확인
            if (userBingoCount >= 3 || aiBingoCount >= 3)
            {
                // 사용자 승리 조건: 3빙고 달성 후 Bingo 버튼을 눌러야 함
                if (userBingoCount >= 3 && bingoButton.interactable)
                {
                    CompleteGame();
                    yield break;
                }

                // AI 승리 조건: 3빙고 달성
                if (aiBingoCount >= 3)
                {
                    CompleteGame();
                    yield break;
                }
            }
        }
    }

    void DisplayNextImage()
    {
        int randomIndex;

        do
        {
            randomIndex = Random.Range(0, imageWordPairs.Count);
        } while (usedImageIndices.Contains(randomIndex));

        usedImageIndices.Add(randomIndex);

        currentPair = imageWordPairs[randomIndex];

        string level = PlayerPrefs.GetString("SelectedLevel", "Level1");
        int stage = PlayerPrefs.GetInt("SelectedStage", 1);
        string imagePath = $"{level}Images-{stage}/{currentPair.Num}";

        currentImageText.text = currentPair.Eng;
        currentImageDisplay.sprite = Resources.Load<Sprite>(imagePath);

        Debug.Log($"Image path for loading: {imagePath}");
        if ( currentImageDisplay.sprite == null )
        {
            Debug.LogError($"Falied to load sprite : {imagePath}");
        }

        Debug.Log($"Displayed Image: {currentPair.Num}, Word: {currentPair.Eng}");
    }

    IEnumerator UserTurn()
    {
        float remainingTime = turnTimeLimit;
        bool choiceMade = false;

        timerText.gameObject.SetActive(true);

        foreach ( var cell in userBoardGenerator.cells)
        {
            cell.button.onClick.AddListener(() =>
            {
                if ( cell.Pair != null && !cell.IsMarked)
                {
                    Debug.Log($"Comparing Cell Pair: {cell.Pair.Eng} with Currnet Pair : {currentPair.Eng}");

                    if (cell.Pair.Eng == currentPair.Eng)
                    {
                        cell.MarkAsCorrectWithSprite("bingo_answer");
                        userBoardGenerator.MarkCorrect(cell.Pair.Num);

                        Transform AnswerOverlay = cell.transform.Find("AnswerOverlay");
                        if (AnswerOverlay != null)
                        {
                            AnswerOverlay.gameObject.SetActive(true);
                        }
                        else
                        {
                            Debug.LogWarning($"Cell {cell.name} dose not have a 'AnswerOverlay' object");
                        }

                        isUserTurn = false;
                        choiceMade = true;
                    }
                    else
                    {
                        Debug.Log("Wrong answer");
                    }
                }
            });
            
        }

        while ( remainingTime > 0 && !choiceMade )
        {
            remainingTime -= Time.deltaTime;
            timerText.text = $"{(int)remainingTime}";
            FillLoding.fillAmount = remainingTime / turnTimeLimit;
            yield return null;
        }

        Debug.Log($"Current ImageWordPair: Num={currentPair.Num}, Eng={currentPair.Eng}");
        foreach ( var cell in userBoardGenerator.cells )
        {
            cell.button.onClick.RemoveAllListeners();
        }
        
        if ( !choiceMade )
        {
            Debug.Log("User failed to make a choice");
        }
    }

    IEnumerator AITurn()
    {
        yield return new WaitForSeconds(1.0f);
        
        ImageWordPair currentPair = imageWordPairs[usedImageIndices.Count - 1];

        bool aiMadeChoice = false;

        foreach ( var cell in aiBoardGenerator.cells)
        {
            if ( cell.Pair != null && cell.Pair.Num == currentPair.Num && !cell.IsMarked )
            {
                if ( Random.value < 0.4f)
                {
                    cell.MarkAsCorrectWithSprite("answer");
                    aiBoardGenerator.MarkCorrect(cell.Pair.Num);
                    aiMadeChoice = true;
                    Debug.Log($"AI chose correct answer : {currentPair.Eng}");
                }
                else
                {
                    Debug.Log("AI skipped this turn");
                }
                break;
            }
        }

        if ( !aiMadeChoice )
        {
            Debug.Log("AI did not make a choice this turn");
        }

        UpdateBingoStatus();

        isUserTurn = true;
    }

    void UpdateBingoStatus()
    {
        userBingoCount = userBoardGenerator.CheckBingoLines();
        aiBingoCount = aiBoardGenerator.CheckBingoLines();

        userResultText.text = $"{userBingoCount} bingo";
        AIResultText.text = $"{aiBingoCount} bingo ";

        if ( userBingoCount >= 3)
        {
            bingoButtonCover.SetActive(false);
            bingoButton.interactable = true;
        }
    }

    void CheckBingoStatus()
    {
        userBingoCount = userBoardGenerator.CheckBingoLines();
        aiBingoCount = aiBoardGenerator.CheckBingoLines();
    }

    void CompleteGame()
    {
        int stage = PlayerPrefs.GetInt("SelectedStage", 1);
        string level = PlayerPrefs.GetString("SelectedLevel", "Level1");
        int coinsEarned = levelCoinMapping.ContainsKey(level) ? levelCoinMapping[level] : 0;

        int totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        totalCoins += coinsEarned;

        PlayerPrefs.SetInt("CoinsEarned", coinsEarned);
        PlayerPrefs.SetString("LastPlayedLevel", levelNameMapping.ContainsKey(level) ? levelNameMapping[level] : "Unknown Level");
        PlayerPrefs.SetString("LastStage", $"Stage {stage}");
        PlayerPrefs.SetInt("TotalCoins", totalCoins);

        PlayerPrefs.Save(); // 반드시 호출

        Debug.Log($"Game Complete! You earned {coinsEarned} coins for {levelNameMapping[level]}.");

        EndGame();
    }

    void EndGame()                        
    {
        int totalCoins = PlayerPrefs.GetInt("TotalCoins", 0) + coinsEarned;

        if ( userBingoCount >=3)
        {
            bingoButtonCover.SetActive(false);
            bingoButton.interactable = true;
            bingoButton.onClick.AddListener(OnBingoButtonClicked);
        }
        else
        {
            SceneManager.Instance.EnableDelay(0.7f, SceneType.Complete);
        }
    }

    void OnExtiButtonClicked()
    {
        SceneManager.Instance.EnableDelay(0.7f, SceneType.Lobby);
    }

    void OnBingoButtonClicked()
    {
        if (userBingoCount >= 3)
        {
            SceneManager.Instance.EnableDelay(0.7f, SceneType.Complete3B);
        }
    }
}
