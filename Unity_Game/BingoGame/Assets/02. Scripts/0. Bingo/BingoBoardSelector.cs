using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#pragma warning disable IDE0051

public class BingoBoardSelector : MonoBehaviour
{
    public Button startButton;

    private int selectedBoardIndex;

    void Start()
    {
        startButton.onClick.AddListener(OnStartButtonClicked);
    }

    void OnStartButtonClicked()
    {
        PlayerPrefs.SetInt("SelectedBingoBoard", selectedBoardIndex);
        SceneManager.Instance.EnableDelay(0.7f, SceneType.BingoGame);
    }
}
