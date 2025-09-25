using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExitController : MonoBehaviour
{

    public Button retryButton;
    public Button ExitButton;

    public TMP_Text coinsText;
    public TMP_Text scoreText;


    void Start()
    {
        int Coins = PlayerPrefs.GetInt("LastGameCoins", 0);
        coinsText.text = "+ " + Coins.ToString();

        int correctAnswer = PlayerPrefs.GetInt("CorrectAnswer", 0);
        int totalQuestions = PlayerPrefs.GetInt("TotalQuestions", 4);

        scoreText.text = correctAnswer + " / " + totalQuestions;

        retryButton.onClick.AddListener(OnRetryButtonClicked);
        ExitButton.onClick.AddListener(() => SceneManager.Instance.EnableDelay(1f, SceneType.Lobby));
    }

    void OnRetryButtonClicked()
    {
        string lastCategory = PlayerPrefs.GetString("LastSelectedCategory", "Level1");

        switch ( lastCategory )
        {
            case "Level1":
                SceneManager.Instance.EnableDelay(1f, SceneType.Level1Game);
                break;
            case "Level2":
                SceneManager.Instance.EnableDelay(1f, SceneType.Level2Game);
                break;
            case "Level3":
                SceneManager.Instance.EnableDelay(1f, SceneType.Level3Game);
                break;
            case "Level4":
                SceneManager.Instance.EnableDelay(1f, SceneType.Level4Game);
                break;
            case "Level5":
                SceneManager.Instance.EnableDelay(1f, SceneType.Level5Game);
                break;
            case "Level6":
                SceneManager.Instance.EnableDelay(1f, SceneType.Level6Game);
                break;
            case "Level7":
                SceneManager.Instance.EnableDelay(1f, SceneType.Level7Game);
                break;
            case "Level8":
                SceneManager.Instance.EnableDelay(1f, SceneType.Level8Game);
                break;
            case "Level9":
                SceneManager.Instance.EnableDelay(1f, SceneType.Level9Game);
                break;
            case "Level10":
                SceneManager.Instance.EnableDelay(1f, SceneType.Level10Game);
                break;
            case "Level11":
                SceneManager.Instance.EnableDelay(1f, SceneType.Level11Game);
                break;
            case "Level12":
                SceneManager.Instance.EnableDelay(1f, SceneType.Level12Game);
                break;
        }
    }
   
}
