using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;


public enum SceneType
{
    None,

    Logo,  // 미니게임 로고

    Login, // 미니게임 로그인

    Lobby, // 미니게임 로비 

    Level1Game,
    Level2Game,
    Level3Game,
    Level4Game,
    Level5Game,
    Level6Game,
    Level7Game,
    Level8Game,
    Level9Game,
    Level10Game,
    Level11Game,
    Level12Game,

    Result, // 미니게임 결과

    Exit // 미니게임 재도전 OR 나가기(로비)
}

public class SceneManager : TSingleton<SceneManager>
{
    private bool loading = false;

    [SerializeField]
    private SceneType current = SceneType.None;

    private Dictionary<SceneType, Scene> sceneList = new Dictionary<SceneType, Scene>();

    
    public T AddScene<T> (SceneType sType, bool state = false) where T : Scene
    {
        if( ! sceneList.ContainsKey(sType) )
        {
            T t = this.CreateObject<T>(transform);
            t.enabled = state;
            sceneList.Add(sType, t);
            return t;
        }

        sceneList[sType].enabled = state;
        return sceneList[sType] as T;
    }

    public void EnableScript(SceneType scene)
    {
        foreach(var pair in sceneList)
        {
            if (pair.Key != scene)
            {
                pair.Value.enabled = false;
            }
            else
                pair.Value.enabled = true;
        }
    }

    public void Enable(SceneType nextScene)
    {
        if ( sceneList.ContainsKey( nextScene))
        {
            if (loading)
                return;

            loading = true;
            EnableScript(nextScene);
            LoadAsync(nextScene);
        }
    }

    public void EnableDelay(float delayTime, SceneType nextScene)
    {
        if(loading) 
            return;
        StartCoroutine(IEEnableDelay(delayTime, nextScene));

    }

    private IEnumerator IEEnableDelay(float delayTime, SceneType nextScene)
    {
       // UIManager.Instance.FadeOut(1);
        yield return new WaitForSeconds(delayTime);
        Enable(nextScene);
       // UIManager.Instance.FadeIn(1);
    }

    private void LoadAsync(SceneType nextScene)
    {
        StartCoroutine(IELoadAsync(nextScene));
    }

    private IEnumerator IELoadAsync(SceneType nextScene) 
    {
        AsyncOperation operation = UnitySceneManager.LoadSceneAsync(nextScene.ToString());

        bool state = false;
        while ( ! state)
        {
            if ( sceneList.ContainsKey(nextScene))
            {
                sceneList[nextScene].Progress(operation.progress);
            }

            if ( operation.isDone)
            {
                state = true;
                if (sceneList.ContainsKey(current))
                    sceneList[current].Exit();

                if (sceneList.ContainsKey(nextScene))
                    sceneList[nextScene].Enter();

                current = nextScene;
                loading = false;
            }

            yield return null;
        }
    }

    // 안드로이드 
    private void OnApplicationFocus(bool focus)
    {

    }

    private void OnApplicationPause(bool pause)
    {

    }

    private void OnApplicationQuit()
    {

    }



}
