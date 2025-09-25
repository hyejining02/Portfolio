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

    public List<Sprite> Level1Sprite = new List<Sprite>();        // �̹��� ����Ʈ(���ҽ� ���� -> �̹��� ����)
    public List<ImageData> Level1Names = new List<ImageData>();   // JSON ���Ͽ��� �ҷ��� �̹��� ���� ����Ʈ

    private List<Results> currentResults = new List<Results>();
    private int currentGameCoins = 0;

    private int currentQuestionIndex;
    private int totalQuestions = 4;
    
    private bool isAnswering;
    private bool isProcessingVoice;

    private List<int> usedIndices = new List<int>(); // �̹� ���� �ε��� ������ ����Ʈ

    private string updateCoinsURL = "http://45.115.155.67:8080/login";

    // �̹��� ������ Ŭ���� ���� (Num�� Eng ����)
    [System.Serializable]

    public class ImageData
    {
        public string Num;  // �̹����� ���� ��ȣ
        public string Eng;  // �̹����� �ش��ϴ� ���� �̸�
    }

    void Start()
    {
        LoadLevel1Images();          // �̹����� �ε�
        LoadLevel1NamesFromJson();   // JSON ���Ͽ��� �̸��� �ε�

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
        isProcessingVoice = false; // �� ������ ������ �� ���� �ν� �Ϸ� ��� ����
    }

    IEnumerator NextQuestionCoroutine()
    {
        yield return new WaitForSeconds(2f); // ��� Ȯ�� �� ��� �ð�

        LoadNextQuestion(); // ���� ������ �̵�
    }

    public void CheckAnswerWithVoice(string voiceText)
    {
        if (isAnswering && !isProcessingVoice)
        {
            isProcessingVoice = true; // ���� �ν� �Ϸ� ��� ����

            // ���� �������� �̹����� ��ȣ�� ã��
            string currentImageNum = GetCurrentImageNum();
            if (currentImageNum != null)
            {
                // JSON �����Ϳ��� �ش� �̹����� Eng ���� ã��
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

    // ���� �̹����� ��ȣ�� ��ȯ�ϴ� �޼���
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

        // ���� ���� ��ȣ�� �ش��ϴ� ���� �̹��� ������Ʈ ��������
        UnityEngine.UI.Image currentNumberGameObject = numberImages[currentQuestionIndex - 1];

        if (currentNumberGameObject != null)
        {
            // ������ ���
            if (isCorrect)
            {
                // _Number�� �����ϴ� ���� ������Ʈ�� ã�Ƽ� Ȱ��ȭ
                Transform numberChild = currentNumberGameObject.transform.Find("_Number" + currentQuestionIndex);
                if (numberChild != null)
                {
                    numberChild.gameObject.SetActive(true); // ���� _NumberX ������Ʈ Ȱ��ȭ
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
                // ����ó��(UI��������)
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
        int correctAnswer = PlayerPrefs.GetInt("CorrectAnswer", 0); // ������ ���� ���� �� ��������
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

        // ��� ������ �̵�
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
        // ���� �ν� ���� (Whisper���� ȣ���ϵ��� ����)
        isProcessingVoice = true;
    }
}
