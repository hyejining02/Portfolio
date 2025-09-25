using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logo : MonoBehaviour
{
    void Start()
    {
        SceneManager.Instance.AddScene<Login>(SceneType.Login);
        SceneManager.Instance.AddScene<Lobby>(SceneType.Lobby);
        SceneManager.Instance.AddScene<KeystoneLV1Game>(SceneType.KeystoneLV1Game);
        SceneManager.Instance.AddScene<KeystoneLV2Game>(SceneType.KeystoneLV2Game);
        SceneManager.Instance.AddScene<KeystoneLV3Game>(SceneType.KeystoneLV3Game);
        SceneManager.Instance.AddScene<KeystoneLV4Game>(SceneType.KeystoneLV4Game);
        SceneManager.Instance.AddScene<ZoomLV1Game>(SceneType.ZoomLV1Game);
        SceneManager.Instance.AddScene<ZoomLV2Game>(SceneType.ZoomLV2Game);
        SceneManager.Instance.AddScene<ZoomLV3Game>(SceneType.ZoomLV3Game);
        SceneManager.Instance.AddScene<ZoomLV4Game>(SceneType.ZoomLV4Game);
        SceneManager.Instance.AddScene<Complete>(SceneType.Complete);

        UIManager.Instance.FadeOutDelay(1.5f, 1);
        SceneManager.Instance.EnableDelay(2.5f, SceneType.Login);
    }

}
