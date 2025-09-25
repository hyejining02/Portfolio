using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Xml.Serialization;

public class LobbyController : MonoBehaviour
{
    public Button startButton;
    public Button exitButton;
    public Button[] categoryButtons;

    public Button easyButton;
    public Button normalButton;
    public Button hardButton;
    public Button popupExitButton;

    public GameObject difficultyPopup;

    public TMP_Text userNameText;
    public TMP_Text coinsText;

    private string selectedCategory;
    public string selectedDifficulty;
    
    #pragma warning disable IDE0051


    void Start()
    {
        string userName = PlayerPrefs.GetString("UserName", "Guest");
        int userCoins = PlayerPrefs.GetInt("UserCoins", 0);

        userNameText.text = " " + userName;
        coinsText.text = " " + userCoins;

        // 각 카테고리 버튼에 리스너 추가
        for ( int i = 0; i < categoryButtons.Length; i++ )
        {
            int index = i;
            categoryButtons[index].onClick.AddListener ( ()=> OnCategoryButtonClicked(index));
        }

        // 난이도 버튼 리스너 추가
        easyButton.onClick.AddListener(() => OnDifficultySelected("easy"));
        normalButton.onClick.AddListener(() => OnDifficultySelected("normal"));
        hardButton.onClick.AddListener(() => OnDifficultySelected("hard"));

        // Start 및 Exit버튼 리스너 추가
        startButton!.onClick.AddListener(OnStartButtonClick);
        exitButton!.onClick.AddListener(OnExitButtonClick);
        popupExitButton!.onClick.AddListener(OnPopupExitButtonClick);

        // 초기 난이도 팝업 비활성화
        difficultyPopup.SetActive(false);

        // 코인 업데이트
        UpdateCoinsText();
    }

    void OnCategoryButtonClicked(int categoryIndex)
    {
        selectedCategory = "Level" + (categoryIndex + 1);
        PlayerPrefs.SetString("SelectedLevel",selectedCategory);

        difficultyPopup.SetActive(true);
    }

    void OnDifficultySelected(string difficulty)
    {
        selectedDifficulty = difficulty;
        PlayerPrefs.SetString("SelectedDifficulty", selectedDifficulty);
        Debug.Log("Selected Difficutly : " + selectedDifficulty);
    }

    public void OnStartButtonClick()
    {
        if ( !string.IsNullOrEmpty( selectedCategory) && !string.IsNullOrEmpty(selectedDifficulty))
        {
            PlayerPrefs.SetString("SelectedLevel", selectedCategory);
            PlayerPrefs.SetString("SelectedDifficulty", selectedDifficulty);

            LoadGameScene();
        }
        else
        {
            Debug.Log("카테고리와 난이도를 선택하세요.");
        }
    }

    public void OnExitButtonClick()
    {
        SceneManager.Instance.EnableDelay(1.3f, SceneType.Login);
    }
    public void OnPopupExitButtonClick()
    {
        difficultyPopup.SetActive(false);
    }

    // 게임씬을 난이도에 맞춰 로드
    void LoadGameScene()
    {
        SceneType sceneType = GetSceneNameByCategory(selectedCategory);
        if ( sceneType != SceneType.None)
        {
            SceneManager.Instance.EnableDelay(1f, sceneType);
        }
        else
        {
            Debug.LogError("잘못된 카테고리입니다.");
        }
    }

    SceneType GetSceneNameByCategory(string category)
    {
        switch (category)
        {
            case "Level1": return SceneType.KeystoneLV1Game;
            case "Level2": return SceneType.KeystoneLV2Game;
            case "Level3": return SceneType.KeystoneLV3Game;
            case "Level4": return SceneType.KeystoneLV4Game;
            case "Level5": return SceneType.ZoomLV1Game;
            case "Level6": return SceneType.ZoomLV2Game;
            case "Level7": return SceneType.ZoomLV3Game;
            case "Level8": return SceneType.ZoomLV4Game;
            default: return SceneType.None; // None은 기본값으로 설정
        }
    }

    void UpdateCoinsText()
    {
        int totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        coinsText.text = "" + totalCoins;
    }
}
