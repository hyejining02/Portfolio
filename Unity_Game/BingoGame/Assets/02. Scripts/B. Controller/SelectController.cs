using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
#pragma warning disable IDE0051

[System.Serializable]
public class BingoBoardData
{
    public List<ImageWordPair> Pairs;
}

public class SelectController : MonoBehaviour
{
    public GameObject bingoBoardsPrefab;
    public Transform bingoBoardContainer;

    public Button leftArrow;
    public Button rightArrow;
    public Button startButton;
    public Button exitButton;

    public TMP_Text levelNameText;
    public TMP_Text stageNameText;

    private int currentBoardIndex = 0;
    private List<GameObject> generatedBoards = new List<GameObject>();
     
    void Start()
    {
        string level = PlayerPrefs.GetString("SelectedLevel", "Unknown Level");
        int stage = PlayerPrefs.GetInt("SelectedStage", 1);
        levelNameText.text = $"{level}-{stage}";
        stageNameText.text = $"Stage "+ stage.ToString();

        GenerateBingoBoard();
        UpdateBoardDisplay();

        leftArrow.onClick.AddListener(OnLeftArrowClicked);
        rightArrow.onClick.AddListener(OnRightArrowClicked);
        startButton.onClick.AddListener(OnStartButtonClicked);
        exitButton.onClick.AddListener(OnExitButtonClicked);
    }

    void GenerateBingoBoard()
    {
        string level = PlayerPrefs.GetString("SelectedLevel","Level1");
        int stage = PlayerPrefs.GetInt("SelectedStage",1);

        string textPath = $"{level}Names-{stage}";

        List<ImageWordPair> pairs = BingoDataLoader.LoadBingoPairs(textPath);

        if ( pairs == null || pairs.Count < 16 )
        {
            Debug.LogError("Not enough word pairs to generate bingo boards.");
            return;
        }

        int boardCount = Random.Range(4, 6);

        for ( int i = 0; i < boardCount; i++ )
        {
            GameObject newBoard = Instantiate(bingoBoardsPrefab, bingoBoardContainer);

            List<ImageWordPair> shuffledPairs = new List<ImageWordPair>(pairs);
            Shuffle(shuffledPairs);

            Debug.Log($"Board {i} generated with pairs: {shuffledPairs.Count}");

            BingoBoardGenerator generator = newBoard.GetComponent<BingoBoardGenerator>();
            generator.PopulateBingoBoard(shuffledPairs, true); // true : 사용자 빙고보드
            newBoard.SetActive(false);
            generatedBoards.Add(newBoard);
        }
    }

    void UpdateBoardDisplay()
    {
        for ( int i = 0; i < generatedBoards.Count; i++ )
        {
            generatedBoards[i].SetActive(i == currentBoardIndex);
        }
    }

    void OnLeftArrowClicked()
    {
        currentBoardIndex = (currentBoardIndex - 1 + generatedBoards.Count) % generatedBoards.Count;
        UpdateBoardDisplay();
    }

    void OnRightArrowClicked()
    {
        currentBoardIndex = (currentBoardIndex + 1) % generatedBoards.Count;
        UpdateBoardDisplay();
    }

    void SaveSelectedBoardData(List<ImageWordPair> selectedPairs)
    {
        BingoBoardData selectedBoardData = new BingoBoardData { Pairs = selectedPairs };
        string jsonData = JsonUtility.ToJson(selectedBoardData);
        PlayerPrefs.SetString("SelectedBoardData", jsonData);
        PlayerPrefs.Save();
    }

    void OnStartButtonClicked()
    {
        PlayerPrefs.SetInt("SelectedBingoBoard", currentBoardIndex);

        BingoBoardGenerator selectedGenerator = generatedBoards[currentBoardIndex].GetComponent<BingoBoardGenerator>();
        List<ImageWordPair> selectedPairs = selectedGenerator.GetBoardPairs();

        // 디버그로그 추가
        Debug.Log($"Selected Board Index : {currentBoardIndex}");
        Debug.Log($"Selected Pairs Count : {selectedPairs.Count}");

        foreach ( var pair in selectedPairs )
        {
            Debug.Log($"Pair : {pair.Num} - {pair.Eng}");
        }

        string boardDataJson = JsonUtility.ToJson(new BingoBoardData { Pairs = selectedPairs });
        PlayerPrefs.SetString("SelectedBoardData", boardDataJson);

        SceneManager.Instance.EnableDelay(0.7f, SceneType.BingoGame);
    }

    void OnExitButtonClicked()
    {
        SceneManager.Instance.EnableDelay(0.7f, SceneType.Lobby);
    }

    void Shuffle<T>(List<T> list)
    {
        for ( int i = 0; i < list.Count; i++ )
        {
            int randomIndex = Random.Range(0, list.Count);
            T temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
