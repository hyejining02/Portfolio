using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logo : MonoBehaviour
{

    void Start()
    {
        SceneManager.Instance.AddScene<Login>(SceneType.Login);
        SceneManager.Instance.AddScene<Lobby>(SceneType.Lobby);

        SceneManager.Instance.AddScene<Level1Game>(SceneType.Level1Game);

        SceneManager.Instance.AddScene<GameResultScene>(SceneType.Result);
        SceneManager.Instance.AddScene<ExitScene>(SceneType.Exit);

        UIManager.Instance.FadeOutDelay(1.5f,1);
        SceneManager.Instance.EnableDelay(2.5f, SceneType.Login);
    }

}
