using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class CompleteController : MonoBehaviour
{
    public GameObject wordCardPrefab;
    public GameObject imageCardPrefab;
    public Transform wordContainer;
    public Transform imageContainer;
    public TMP_Text coinText;

    public Button ExitButton;
    public Button RetryButton;

    void Start()
    {
        int totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        coinText.text = "+" + totalCoins;

        ExitButton.onClick.AddListener(() => SceneManager.Instance.EnableDelay(1.5f, SceneType.Lobby));
    }
    
    public void OnRestartButtonClicked()
    {
        string lastCategory = PlayerPrefs.GetString("LastSelectedCategory", "Level1");
        switch ( lastCategory )
        {
            case "Level1":
                SceneManager.Instance.EnableDelay(1f, SceneType.ZoomLV1Game); 
                break;
            case "Level2":
                SceneManager.Instance.EnableDelay(1f, SceneType.ZoomLV2Game);
                break;
            case "Level3":
                SceneManager.Instance.EnableDelay(1f, SceneType.ZoomLV3Game);
                break;
            case "Level4":
                SceneManager.Instance.EnableDelay(1f, SceneType.ZoomLV4Game);
                break;
            case "Level5":
                SceneManager.Instance.EnableDelay(1f, SceneType.KeystoneLV1Game);
                break;
            case "Level6":
                SceneManager.Instance.EnableDelay(1f, SceneType.KeystoneLV2Game);
                break;
            case "Level7":
                SceneManager.Instance.EnableDelay(1f, SceneType.KeystoneLV3Game);
                break;
            case "Level8":
                SceneManager.Instance.EnableDelay(1f, SceneType.KeystoneLV4Game);
                break;
        }
    }

}
