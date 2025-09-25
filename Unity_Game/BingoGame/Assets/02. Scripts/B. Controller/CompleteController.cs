using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
#pragma warning disable IDE0051


public class CompleteController : MonoBehaviour
{
    public TMP_Text categoryText;
    public TMP_Text levelText;
    public TMP_Text earnedCoinsText;

    public Button retryButton;
    public Button exitButton;

    private string lastCategory;
    private int lastStage;
    private int coinsEarned;
    private int totalCoins;

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
        StartCoroutine(UpdateDataDelay());

        retryButton.onClick.AddListener(OnRetryButtonClicked);
        exitButton.onClick.AddListener(OnExitButtonClicked);
    }

    IEnumerator UpdateDataDelay()
    {
        yield return new WaitForSeconds(0.1f);

        string lastCategory = PlayerPrefs.GetString("LastStage", "Stage 0");
        string lastLevel = PlayerPrefs.GetString("LastPlayedLevel", "Default Level");
        int coinsEarned = PlayerPrefs.GetInt("CoinsEarned", 0);
        int totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);

        totalCoins += coinsEarned;
        PlayerPrefs.SetInt("TotalCoins", totalCoins);
        PlayerPrefs.Save(); 

        Debug.Log($"Loaded data : Category = {lastCategory}, Level = {lastLevel}, Coins Earned = {coinsEarned}");

        categoryText.text = lastCategory;
        levelText.text = lastLevel;
        earnedCoinsText.text = $"+{coinsEarned}";

    }

    void LoadCategoryData()
    {
        string selectedLevel = PlayerPrefs.GetString("SelectedLevel", "Level1");

        if (levelNameMapping.TryGetValue(selectedLevel, out string levelName))
        {
            currentLevelName = levelName;
        }
        else
        {
            currentLevelName = "Unknown Level";
        }
    }

    void LoadGetCoins()
    {
        string coinsEarned = PlayerPrefs.GetString("CoinsEarned", "Level1");

        if (levelCoinMapping.TryGetValue(coinsEarned, out int currentGetCoins))
        {
            currentCoins = currentGetCoins;
        }
    }
    
    void OnRetryButtonClicked()
    {
        string lastSelectedLevel = PlayerPrefs.GetString("SelectedLevel", "Level1");
        int lastSelectedStage = PlayerPrefs.GetInt("SelectedStage", 1);

        PlayerPrefs.SetString("SelectedLevel",lastSelectedLevel);
        PlayerPrefs.SetInt("SelectedStage", lastSelectedStage);

        SceneManager.Instance.EnableDelay(0.7f, SceneType.SelectBoard);
    }

    void OnExitButtonClicked()
    {
        PlayerPrefs.SetInt("CoinsEarned", 0);
        PlayerPrefs.Save();

        SceneManager.Instance.EnableDelay(0.7f, SceneType.Lobby);
    }
}
