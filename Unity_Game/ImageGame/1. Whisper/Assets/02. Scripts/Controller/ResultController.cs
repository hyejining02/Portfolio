using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Newtonsoft.Json;

public class ResultController : MonoBehaviour
{
    public Image[] questionImages;
    public TMP_Text[] questionTexts;

    public Button nextButton;

    private List<Results> allResults;

    void Start()
    {
        string lastSelectedCategory = PlayerPrefs.GetString("LastSelectedCategory", "");

        string resultsJson = PlayerPrefs.GetString(lastSelectedCategory + "Results", "[]");

        Debug.Log(lastSelectedCategory + "Results loaded: " + resultsJson);

        allResults = JsonConvert.DeserializeObject<List<Results>>(resultsJson);

        DisplayResults();

        nextButton.onClick.AddListener(OnNextButtonClicked);
    }

  
    void DisplayResults() // 문제 및 정답을 화면에 표시
    {
        for ( int i = 0; i < questionImages.Length && i < allResults.Count; i ++)
        {
            questionImages[i].sprite = Resources.Load<Sprite>(allResults[i].imagePath);

            questionTexts[i].text = allResults[i].correctAnswer;
        }

    }

    void OnNextButtonClicked()
    {
        SceneManager.Instance.EnableDelay(1.3f, SceneType.Exit);
    }
}
