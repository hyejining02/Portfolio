using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using Newtonsoft.Json;
using UnityEngine.Networking;
using Debug = UnityEngine.Debug;
using static System.Net.Mime.MediaTypeNames;

#pragma warning disable IDE0051

public class Level1GameController : MonoBehaviour
{

    public UnityEngine.UI.Image Level1Images;
    public UnityEngine.UI.Image[] numberImages;

    public TMP_Text timerText;

    public Button Answer1Button;
    public Button Answer2Button;
    public Button Answer3Button;    // 정답선택 버튼 3개

    public List<Sprite> Level1Sprite = new List<Sprite>();        // 이미지 리스트(리소스 폴더 -> 이미지 폴더)
    public List<ImageData> Level1Names = new List<ImageData>();   // JSON 파일에서 불러온 이미지 정보 리스트

    private List<Results> currentResults = new List<Results>();
    private int currentGameCoins = 0;

    private int currentQuestionIndex;
    private int totalQuestions = 4;

    private bool isAnswering;

    public GameObject[] numberIndices;
    private List<int> usedIndices = new List<int>(); // 이미 사용된 인덱스 추적용 리스트

    private string updateCoinsURL = "http://45.115.155.67:8080/login";

    // 이미지 데이터 클래스 정의 (Num과 Eng 포함)
    [System.Serializable]
    public class ImageData
    {
        public string Num;  // 이미지의 고유 번호
        public string Eng;  // 이미지에 해당하는 영어 이름
    }

    void Start()
    {
        LoadLevel1Images();          // 이미지를 로드
        LoadLevel1NamesFromJson();   // JSON 파일에서 이름을 로드

        PlayerPrefs.SetInt("CorrectAnswer", 0);

        currentQuestionIndex = 0;

        LoadNextQuestion();
    }

    public void StartFillLoding(float duration)
    {
        StartCoroutine(UpdateFillLoding(duration));
    }

    IEnumerator UpdateFillLoding(float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            int remainingTime = Mathf.CeilToInt(duration - elapsedTime);
            timerText.text = "Time : " + remainingTime;

            if (remainingTime <= 0)
            {
                timerText.text = "Time : 0";
                //OnTimeExpired();  // 시간이 다 되었을 때 처리
                yield break;
            }

            yield return null;
        }
    }

    void LoadLevel1Images()
    {
        Level1Sprite.AddRange(Resources.LoadAll<Sprite>("Level1Images"));
        if (Level1Sprite.Count == 0)
        {
            UnityEngine.Debug.LogError("No images found in Resources/Level1Images");
        }
    }

    void LoadLevel1NamesFromJson()
    {
        TextAsset jsonTextAsset = Resources.Load<TextAsset>("Level1Names");
        if (jsonTextAsset != null)
        {
            Level1Names = JsonConvert.DeserializeObject<List<ImageData>>(jsonTextAsset.text);
        }
        else
        {
            UnityEngine.Debug.LogError("No names JSON file found in Resources/Level1Names");
        }
    }

    void LoadNextQuestion()
    {
        if (currentQuestionIndex >= totalQuestions)
        {
            EndGame();
            return;
        }

        isAnswering = false;

        int randomIndex;
        int maxAttempts = 5;
        int attemptCounter = 0;

        do
        {
            randomIndex = UnityEngine.Random.Range(0, Level1Sprite.Count);
            attemptCounter++;

            if (attemptCounter > maxAttempts)
            {
                Debug.LogError("Too many attempts to find a unique random index");
                break;
            }
        }
        while (usedIndices.Contains(randomIndex));

        usedIndices.Add(randomIndex);
        Debug.Log("Seletced random index : " + randomIndex);

        Level1Images.sprite = Level1Sprite[randomIndex];

        // 현재 문제 보여줌
        for (int i = 0; i < numberIndices.Length; i++)
        {
            numberIndices[i].SetActive(i == currentQuestionIndex);
        }

        string correctAnswer = GetCurrentImageEng(randomIndex);

        List<string> options = new List<string>();
        options.Add(correctAnswer);

        // 랜덤으로 보기 3개 생성
        while ( options.Count < 3)
        {
            int index = UnityEngine.Random.Range(0, Level1Names.Count);
            string randomOption = Level1Names[index].Eng;
            if( !options.Contains(randomOption))
            {
                options.Add(randomOption);
            }
        }

        options.Shuffle();

        Answer1Button.GetComponentInChildren<TMP_Text>().text = options[0];
        Answer2Button.GetComponentInChildren<TMP_Text>().text = options[1];
        Answer3Button.GetComponentInChildren<TMP_Text>().text = options[2];

        Answer1Button.onClick.RemoveAllListeners();
        Answer2Button.onClick.RemoveAllListeners();
        Answer3Button.onClick.RemoveAllListeners();

        Answer1Button.onClick.AddListener(()=> OnAnswerSelected(options[0] == correctAnswer, correctAnswer));
        Answer2Button.onClick.AddListener(()=> OnAnswerSelected(options[1] == correctAnswer, correctAnswer));
        Answer3Button.onClick.AddListener(()=> OnAnswerSelected(options[2] == correctAnswer, correctAnswer));

        StartFillLoding(10f);

        isAnswering = true;

        currentQuestionIndex++;
    }

    string GetCurrentImageEng(int index)
    {
        string currentSpriteName = Level1Sprite[index].name;
        ImageData matchedImageData = Level1Names.Find(item => item.Num == currentSpriteName);
        if ( matchedImageData != null )
        {
            return matchedImageData.Eng;
        }
        return null;
    }

    void OnAnswerSelected(bool isCorrect, string correctAnswer)
    {

        if (currentQuestionIndex - 1 >= 0 && currentQuestionIndex - 1 < numberImages.Length)
        {
            UnityEngine.UI.Image currentNumberGameObject = numberImages[currentQuestionIndex - 1];

            if (currentNumberGameObject != null)
            {
                if (isCorrect)
                {
                    Transform numberChild = currentNumberGameObject.transform.Find("_Number" + currentQuestionIndex);
                    if (numberChild != null)
                    {
                        numberChild.gameObject.SetActive(true);
                        currentGameCoins += 50;

                        OnCorrectAnswer();
                    }
                    else
                    {
                        Debug.LogError("_Number" + currentQuestionIndex + "object not found");
                    }
                }
            }
        }
        else
        {
            Debug.LogError("Index out of range for numberImages array");
        }

        currentResults.Add(new Results
        {
            imagePath = "Level1Images/" + Level1Images.sprite.name,
            correctAnswer = correctAnswer,
            category = "Level1"
        });

        if ( currentQuestionIndex >= totalQuestions)
        {
            EndGame();
        }
        else
        {
            StartCoroutine(NextQuestionCoroutine());
        }
    }

    IEnumerator NextQuestionCoroutine()
    {
        yield return new WaitForSeconds(1.3f);
        LoadNextQuestion();
    }

    void OnCorrectAnswer()
    {
        int correctAnswer = PlayerPrefs.GetInt("CorrectAnswer", 0);
        correctAnswer++;
        PlayerPrefs.SetInt("CorrectAnswer", correctAnswer);
    }

    void EndGame()
    {
        string Level1ResultsJson = JsonConvert.SerializeObject(currentResults);
        PlayerPrefs.SetString("Level1Results", Level1ResultsJson);

        int totalCoins = PlayerPrefs.GetInt("TotalCoins", 0) + currentGameCoins;
        PlayerPrefs.SetInt("TotalCoins", totalCoins);

        PlayerPrefs.SetInt("LastGameCoins", currentGameCoins);
        PlayerPrefs.SetString("LastSelectedCategory", "Level1");

        StartCoroutine(UpdateCoinsRequest(currentGameCoins));

        SceneManager.Instance.EnableDelay(0.7f, SceneType.Result);
    }
    IEnumerator UpdateCoinsRequest(int coinsToAdd)
    {
        string authToken = PlayerPrefs.GetString("AuthToken", null);

        if (string.IsNullOrEmpty(authToken))
        {
            Debug.LogError("AuthToken is missing");
            yield break;
        }

        var requestData = new
        {
            token = authToken,
            coins = coinsToAdd
        };

        string jsonData = JsonUtility.ToJson(requestData);

        UnityWebRequest request = new UnityWebRequest(updateCoinsURL, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Coin update success: " + request.downloadHandler.text);
        }
        else
        {
            Debug.LogError("Coin update failed: " + request.error);
        }
    }
}
