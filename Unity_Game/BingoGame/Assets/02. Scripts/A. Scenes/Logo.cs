using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logo : MonoBehaviour
{
    void Start()
    {
        SceneManager.Instance.AddScene<Login>(SceneType.Login);
        SceneManager.Instance.AddScene<Lobby>(SceneType.Lobby);

        SceneManager.Instance.AddScene<SelectBoard>(SceneType.SelectBoard);
        SceneManager.Instance.AddScene<BingoGame>(SceneType.BingoGame);
        SceneManager.Instance.AddScene<Complete>(SceneType.Complete);
        SceneManager.Instance.AddScene<Complete3B>(SceneType.Complete3B);

        UIManager.Instance.FadeOutDelay(1.5f, 1);
        SceneManager.Instance.EnableDelay(2.5f, SceneType.Login);
    }

}
