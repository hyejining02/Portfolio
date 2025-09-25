using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;

//public class MicrophonePermissionRequester : MonoBehaviour
//{
//    void Start()
//    {
//        if (!Application.HasUserAuthorization(UserAuthorization.Microphone))
//        {
//            Application.RequestUserAuthorization(UserAuthorization.Microphone);
//        }
//    }
//}

public class LoginController : MonoBehaviour
{
    public TMP_InputField userIDInputField;
    public TMP_InputField passwordInputField;
    public TMP_Text feedbackText;
    public Button loginButton;


    //private string loginURL = "http://192.168.0.137:8080/login";
    private string loginURL = "http://45.115.155.67:8080/login";

  
    void Start()
    {
        loginButton.onClick.AddListener(OnLoginButtonClick);
    }

    public void OnLoginButtonClick()
    {
        string loginId = userIDInputField.text;
        string loginPwd = passwordInputField.text;

        StartCoroutine(LoginRequest(loginId, loginPwd));
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
                if ( int.TryParse(response.studentCoin, out totalCoins) )
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

    // 코인획득 코루틴 ( 사용보류 )
    //IEnumerator GetUserInfo(string studentCoin)
    //{
    //    string userInfoURL = "http://192.168.0.137:8080/login";

    //    UnityWebRequest request = new UnityWebRequest(userInfoURL, "POST");
    //    request.SetRequestHeader("Authorization", "Bearer " + studentCoin);
    //    request.downloadHandler = new DownloadHandlerBuffer();

    //    yield return request.SendWebRequest();

    //    if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
    //    {
    //        Debug.LogError("Failed to get user info: " + request.error);
    //        feedbackText.text = "Failed to get user info: " + request.error;
    //    }
    //    else
    //    {
    //        string responseText = request.downloadHandler.text;
    //        Debug.Log("User info response: " + responseText);

    //        var response = JsonConvert.DeserializeObject<CoinsResponse>(responseText);

    //        if (response.resultCode == 200)
    //        {
    //            //PlayerPrefs.SetInt("TotalCoins", response.coins);
                
    //        }
    //        else
    //        {
    //            Debug.LogError("Failed to get user info: " + response.resultMsg);
    //            feedbackText.text = "Failed to get user info: " + response.resultMsg;
    //        }
    //    }
    //}
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