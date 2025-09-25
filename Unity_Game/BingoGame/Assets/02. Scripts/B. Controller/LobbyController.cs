using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#pragma warning disable IDE0051

public class LobbyController : MonoBehaviour
{
    public Button[] categoryButtons;
    public Button[] stageButtons;

    public GameObject exitPopup;
    public Button exitButton;
    public Button exitYesButton;
    public Button exitNoButton;

    public GameObject stagePopup;
    public Button closePopupButton;

    public TMP_Text userNameText;
    public TMP_Text coinsText;

    private string selectedLevel;

    void Start()
    {
        string userName = PlayerPrefs.GetString("UserName", "Guest");
        userNameText.text = userName;

        UpdateCoinsText();

        stagePopup.SetActive(false);
        closePopupButton.onClick.AddListener( ()=> stagePopup.SetActive(false) );
        exitButton.onClick.AddListener(OnExitButtonClick);
        exitNoButton.onClick.AddListener(OnExitNoButtonClick);
    }

    public void OnLevelButtonClicked( string level )
    {
        selectedLevel = level;
        stagePopup.SetActive(true);
    }

    public void OnStageButtonClicked( int stage )
    {
        PlayerPrefs.SetString("SelectedLevel", selectedLevel);
        PlayerPrefs.SetInt("SelectedStage", stage);

        SceneManager.Instance.EnableDelay(0.7f, SceneType.SelectBoard);
    }

    void UpdateCoinsText()
    {
        int totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        int coinsEarned = PlayerPrefs.GetInt("CoinsEarned", 0);

        coinsText.text = "" + totalCoins;
    }

    public void OnExitButtonClick()
    {
        exitPopup.SetActive(true);
    }

    [System.Obsolete]
    public void OnExitYesButtonClick()
    {
#if UNITY_WEBGL
        Application.ExternalEval("window.close();");
#endif
        Application.Quit();
    }

    public void OnExitNoButtonClick()
    {
        exitPopup.SetActive(false);
    }
}
