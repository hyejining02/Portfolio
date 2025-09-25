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

public class Level1GameController : MonoBehaviour
{
    public UnityEngine.UI.Image FillLoding;
    public UnityEngine.UI.Image Level1Images;
    public UnityEngine.UI.Image[] numberImages;

    public TMP_Text timerText;

    public GameObject[] numberIndices;

    public List<Sprite> Level1Sprite = new List<Sprite>();        // 이미지 리스트(리소스 폴더 -> 이미지 폴더)
    public List<ImageData> Level1Names = new List<ImageData>();   // JSON 파일에서 불러온 이미지 정보 리스트

    private List<Results> currentResults = new List<Results>();
    private int currentGameCoins = 0;

    private int currentQuestionIndex;
    private int totalQuestions = 4;
    
    private bool isAnswering;
    private bool isProcessingVoice;

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
        isAnswering = true;
        isProcessingVoice = false;

        FillLoding.fillAmount = 1;
        timerText.text = " ";

        LoadNextQuestion();
    }

    public void StartFillLoding(float duration)
    {
        StartCoroutine(UpdateFillLoding(duration));
    }

    IEnumerator UpdateFillLoding(float duration)
    {
        float elapsedTime = 0f;
        while ( elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            FillLoding.fillAmount = 1 - (elapsedTime / duration);

            int remainingTime = Mathf.CeilToInt(duration - elapsedTime);
            timerText.text = "Time : " + remainingTime;
            yield return null;
        }

        FillLoding.fillAmount = 0;
        timerText.text = "Time : 0 ";
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

        for (int i = 0; i < numberIndices.Length; i++)
        {
            numberIndices[i].SetActive(i == currentQuestionIndex);
        }

        currentQuestionIndex++;
        isAnswering = true;
        isProcessingVoice = false; // 새 문제를 시작할 때 음성 인식 완료 대기 해제
    }

    IEnumerator NextQuestionCoroutine()
    {
        yield return new WaitForSeconds(2f); // 결과 확인 후 대기 시간

        LoadNextQuestion(); // 다음 문제로 이동
    }

    public void CheckAnswerWithVoice(string voiceText)
    {
        if (isAnswering && !isProcessingVoice)
        {
            isProcessingVoice = true; // 음성 인식 완료 대기 시작

            // 현재 보여지는 이미지의 번호를 찾음
            string currentImageNum = GetCurrentImageNum();
            if (currentImageNum != null)
            {
                // JSON 데이터에서 해당 이미지의 Eng 값을 찾음
                ImageData currentImageData = Level1Names.Find(item => item.Num == currentImageNum);

                if (currentImageData != null)
                {
                    bool isCorrect = string.Equals(voiceText, currentImageData.Eng, System.StringComparison.OrdinalIgnoreCase);
                    OnAnswerSelected(isCorrect, currentImageData.Eng);
                }
            }
            else
            {
                Debug.LogError("Could not find matching image in JSON data");
            }
        }
    }

    // 현재 이미지의 번호를 반환하는 메서드
    string GetCurrentImageNum()
    {
        string currentSpriteName = Level1Images.sprite.name;
        ImageData matchedImageData = Level1Names.Find(item => item.Num == currentSpriteName);
        if (matchedImageData != null)
        {
            return matchedImageData.Num;
        }
        return null;
    }

    void OnAnswerSelected(bool isCorrect, string correctAnswer)
    {
        isAnswering = false;

        // 현재 문제 번호에 해당하는 숫자 이미지 오브젝트 가져오기
        UnityEngine.UI.Image currentNumberGameObject = numberImages[currentQuestionIndex - 1];

        if (currentNumberGameObject != null)
        {
            // 정답일 경우
            if (isCorrect)
            {
                // _Number로 시작하는 하위 오브젝트를 찾아서 활성화
                Transform numberChild = currentNumberGameObject.transform.Find("_Number" + currentQuestionIndex);
                if (numberChild != null)
                {
                    numberChild.gameObject.SetActive(true); // 하위 _NumberX 오브젝트 활성화
                    currentGameCoins += 50;

                    OnCorrectAnswer();
                }
                else
                {
                    Debug.LogError("_Number" + currentQuestionIndex + " object not found.");
                }
            }
            else
            {
                // 오답처리(UI변동없음)
            }
        }
        else
        {
            Debug.LogError("numberIndices[" + (currentQuestionIndex - 1) + "] does not have an Image component.");
        }

        Debug.Log(isCorrect ? "Correct!" : "Wrong!");

        currentResults.Add(new Results
        {
            imagePath = "Level1Images/" + Level1Images.sprite.name,
            correctAnswer = correctAnswer,
            category = "Level1"
        });

        StartCoroutine(NextQuestionCoroutine());
    }
    void OnCorrectAnswer()
    {
        int correctAnswer = PlayerPrefs.GetInt("CorrectAnswer", 0); // 기존에 맞춘 문제 수 가져오기
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

        // 결과 씬으로 이동
        SceneManager.Instance.EnableDelay(0.7f, SceneType.Result);
    }

    IEnumerator UpdateCoinsRequest(int coinsToAdd)
    {
        string authToken = PlayerPrefs.GetString("AuthToken");

        if (string.IsNullOrEmpty(authToken))
        {
            UnityEngine.Debug.LogError("AuthToken is missing");
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
            UnityEngine.Debug.Log("Coin update success: " + request.downloadHandler.text);
        }
        else
        {
            UnityEngine.Debug.LogError("Coin update failed: " + request.error);
        }
    }

    private void OnStartVoiceRecognition()
    {
        // 음성 인식 시작 (Whisper에서 호출하도록 설정)
        isProcessingVoice = true;
    }
}
