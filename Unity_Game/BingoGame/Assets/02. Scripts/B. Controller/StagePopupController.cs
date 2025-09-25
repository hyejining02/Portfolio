using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#pragma warning disable IDE0051

public class StagePopupController : MonoBehaviour
{
    public Button stage1Button;
    public Button stage2Button;
    public Button stage3Button;
    public Button stage4Button;

    public LobbyController lobbyController;

    void Start()
    {
        stage1Button.onClick.AddListener( ()=> lobbyController.OnStageButtonClicked(1) );
        stage2Button.onClick.AddListener( ()=> lobbyController.OnStageButtonClicked(2) );
        stage3Button.onClick.AddListener( ()=> lobbyController.OnStageButtonClicked(3) );
        stage4Button.onClick.AddListener( ()=> lobbyController.OnStageButtonClicked(4) );
    }
}
