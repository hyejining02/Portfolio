using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class LobbyController : MonoBehaviour
{
    public Button startButton;

    public Button[] categoryButtons;

    public TMP_Text userNameText;
    public TMP_Text coinsText;

    private string selectedCategory;

    void Start()
    {
        RequestMicrophonePermission();

        string userName = PlayerPrefs.GetString("UserName", "Guest");
        int userCoins = PlayerPrefs.GetInt("UserCoins", 0);

        userNameText.text = " " + userName;
        coinsText.text = " " + userCoins;

        for (int i = 0; i < categoryButtons.Length; i++)
        {
            int index = i;
            categoryButtons[index].onClick.AddListener(() => OnCategoryButtonClicked(index));
        }

        startButton!.onClick.AddListener(OnStartButtonClick);

        UpdateCoinsText();

    }

    private void RequestMicrophonePermission()
    {
        if (!Microphone.devices.Length.Equals(0))
        {
            Debug.Log("Requesting microphone permission...");
            Microphone.Start(null, true, 1, 44100);
            Microphone.End(null); 
        }
        else
        {
            Debug.LogWarning("No microphone devices found.");
        }
    }

    void OnCategoryButtonClicked(int categoryIndex)
    {
        selectedCategory = "Level"+(categoryIndex+1);
    }

    public void OnStartButtonClick()
    {
        if ( selectedCategory != null )
        {
            switch (selectedCategory)
            {
                case "Level1":
                    SceneManager.Instance.EnableDelay(1.3f, SceneType.Level1Game);
                    break;
                case "Level2":
                    SceneManager.Instance.EnableDelay(1.3f, SceneType.Level2Game);
                    break;
                case "Level3":
                    SceneManager.Instance.EnableDelay(1.3f, SceneType.Level3Game);
                    break;
                case "Level4":
                    SceneManager.Instance.EnableDelay(1.3f, SceneType.Level4Game);
                    break;
                case "Level5":
                    SceneManager.Instance.EnableDelay(1.3f, SceneType.Level5Game);
                    break;
                case "Level6":
                    SceneManager.Instance.EnableDelay(1.3f, SceneType.Level6Game);
                    break;
                case "Level7":
                    SceneManager.Instance.EnableDelay(1.3f, SceneType.Level7Game);
                    break;
                case "Level8":
                    SceneManager.Instance.EnableDelay(1.3f, SceneType.Level8Game);
                    break;
                case "Level9":
                    SceneManager.Instance.EnableDelay(1.3f, SceneType.Level9Game);
                    break;
                case "Level10":
                    SceneManager.Instance.EnableDelay(1.3f, SceneType.Level10Game);
                    break;
                case "Level11":
                    SceneManager.Instance.EnableDelay(1.3f, SceneType.Level11Game);
                    break;
                case "Level12":
                    SceneManager.Instance.EnableDelay(1.3f, SceneType.Level12Game);
                    break;

            }
        }
        else
        {
            Debug.Log("No category selected");
        }
    }


    void UpdateCoinsText()
    {
        int totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        coinsText.text = "" + totalCoins;
    }
}
