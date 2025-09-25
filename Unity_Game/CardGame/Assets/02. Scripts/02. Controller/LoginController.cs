using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;

public class LoginController : MonoBehaviour
{
    public TMP_InputField userIDInputField;
    public TMP_InputField passwordInputField;
    public TMP_Text feedbackText;

    public Button loginButton;
    public Button guestLoginButton;
    public Button guestLoginPopExit;

    public GameObject guestLoginPopup;
    public GameObject guestNamePopup;
    public TMP_InputField guestIDInputField;
    public TMP_InputField guestPWInputField;
    public Button guestEnterButton;

    public Button exitButton;
    public GameObject exitPopup;
    public Button exitYesButton;
    public Button exitNoButton;

    //private string loginURL = "http://192.168.0.137:8080/login";
    private string loginURL = "http://45.115.155.67:8080/login";

    void Start()
    {
        loginButton.onClick.AddListener(OnLoginButtonClick);

        guestLoginButton!.onClick.AddListener(OnGuestLoginButtonClick); //비회원 입장
        guestEnterButton.onClick!.AddListener(OnGuestEnterButtonClick);
        guestLoginPopExit!.onClick.AddListener(OnGuestLoginExitButtonClick);

        exitButton!.onClick.AddListener(OnExitButtonClick);
        exitYesButton!.onClick.AddListener(OnExitYesButtonClick);
        exitNoButton!.onClick.AddListener(OnExitNoButtonClick);

        guestLoginPopup.SetActive(false);
        exitPopup.SetActive(false);
    }

    public void OnLoginButtonClick()
    {
        string loginId = userIDInputField.text;
        string loginPwd = passwordInputField.text;

        StartCoroutine(LoginRequest(loginId, loginPwd));
    }

    public void OnExitButtonClick()
    {
        exitPopup.SetActive(true);
    }

    public void OnExitYesButtonClick()
    {
#if UNITY_WEBGL
        Application.ExternalEval("window.close();");
#endif
        Application.Quit();
    }

    public void OnExitNoButtonClick()
    {
        exitPopup?.SetActive(false);
    }

    public void OnGuestLoginButtonClick()
    {
        guestLoginButton.SetActive(true);
        guestLoginPopup.SetActive(true);
    }

    public void OnGuestEnterButtonClick()
    {
        string guestIDLogin = guestIDInputField.text;
        string guestPWLogin = guestPWInputField.text;

        PlayerPrefs.SetInt("IsGuest", 1);
        PlayerPrefs.SetInt("TotalCoins", 0);
        PlayerPrefs.SetString("UserID", "guest");
        PlayerPrefs.SetString("UserName", guestIDLogin);

        StartCoroutine(GuestLoginRequest(guestIDLogin, guestPWLogin));

        SceneManager.Instance.EnableDelay(1.3f, SceneType.Lobby);
    }

    public void OnGuestLoginExitButtonClick()
    {
        guestLoginPopup?.SetActive(false);
    }

    IEnumerator LoginRequest(string loginId, string loginPwd)
    {
        var requestData = new
        {
            loginId = loginId,
            loginPwd = loginPwd
        };

        string jsonData = JsonConvert.SerializeObject(requestData);

        UnityWebRequest request = new UnityWebRequest(loginURL, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Login failed: " + request.error);
            feedbackText.text = "Login failed: " + request.error;
        }
        else
        {
            string responseText = request.downloadHandler.text;
            Debug.Log("Login response: " + responseText);

            var response = JsonConvert.DeserializeObject<LoginResponse>(responseText);

            if (response.resultCode == 200)
            {
                PlayerPrefs.SetString("AuthToken", response.authToken);
                PlayerPrefs.SetString("UserID", response.studentId);
                PlayerPrefs.SetString("UserName", response.studentName);

                int totalCoins;

                if (int.TryParse(response.studentCoin, out totalCoins))
                {
                    PlayerPrefs.SetInt("TotalCoins", totalCoins);
                }
                else
                {
                    Debug.LogError("Failed to parse studentCoin :" + response.studentCoin);
                    feedbackText.text = "Failed to parse studentCoin";
                }

                SceneManager.Instance.EnableDelay(1.3f, SceneType.Lobby);
            }
            else
            {
                Debug.LogError("Login failed: " + response.resultMsg);
                feedbackText.text = "Login failed: " + response.resultMsg;
            }
        }
    }

    IEnumerator GuestLoginRequest(string guestIDLogin, string guestPWLogin)
    {
        var requestData = new
        {
            loginID = guestIDLogin,
            loginPW = guestPWLogin
        };

        string jsonData = JsonConvert.SerializeObject(requestData);

        UnityWebRequest request = new UnityWebRequest(loginURL, "POST");
        byte[] bodyRow = System.Text.Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRow);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Cotent-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Guest login failed : " + request.error);
            feedbackText.text = "Guest login failed " + request.error;
        }
        else
        {
            string responseText = request.downloadHandler.text;
            Debug.Log("Guest login response " + responseText);

            var response = JsonConvert.DeserializeObject<LoginResponse>(responseText);

            if (response.resultCode == 200)
            {
                PlayerPrefs.SetString("AuthToken", response.authToken);
                PlayerPrefs.SetString("UserID", response.studentId);
                PlayerPrefs.SetString("UserName", response.studentName);

                int totalCoins;
                if (int.TryParse(response.studentCoin, out totalCoins))
                {
                    PlayerPrefs.SetInt("TotalCoins", totalCoins);
                }
                else
                {
                    Debug.LogError("Failed to parse studentCoin : " + response.studentCoin);
                    feedbackText.text = "Failed to parse studentCoin";
                }

                SceneManager.Instance.EnableDelay(1.3f, SceneType.Lobby);
            }
            else
            {
                Debug.LogError("Guest login failed : " + response.resultMsg);
                feedbackText.text = "Guest login failed " + response.resultMsg;
            }
        }
    }
}

[System.Serializable]
public class LoginResponse
{
    public string educat_cd;
    public string studentId;
    public string studentName;
    public string authToken;
    public string studentCoin;
    public int resultCode;
    public string resultMsg;
}