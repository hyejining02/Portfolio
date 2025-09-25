using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public enum UIType
{
    UIFade,
    UILoading,
}

public class UIManager : TSingleton<UIManager>
{
    private readonly string path = "Prefabs/";

    private Dictionary<UIType, UIBase> uiList = new Dictionary<UIType, UIBase>();

    public override void Initialize()
    {
        gameObject.AddComponent<EventSystem>();
        gameObject.AddComponent<StandaloneInputModule>();

        Add<UIFade>(UIType.UIFade, false);
    }

    public T Get<T>(UIType uiType) where T : UIBase
    {
        if (uiList.ContainsKey(uiType))
            return uiList[uiType] as T;

        return null;
    }

    public void FadeInDelay(float delayTime, float targetTime)
    {
        StartCoroutine(IEFadeInDelay(delayTime, targetTime));
    }

    private IEnumerator IEFadeInDelay(float delayTime, float targetTime)
    {
        yield return new WaitForSeconds(delayTime);
        FadeIn(targetTime);
    }

    public void FadeOutDelay(float delayTime, float targetTime)
    {
        StartCoroutine(IEFadeOutDelay(delayTime, targetTime));
    }

    private IEnumerator IEFadeOutDelay(float delayTime, float targetTime)
    {
        yield return new WaitForSeconds(delayTime);
        FadeOut(targetTime);
    }

    public void FadeIn(float targetTime)
    {
        UIFade fade = Get<UIFade>(UIType.UIFade);
        if (fade != null)
            fade.FadeIn(false, targetTime);
    }

    public void FadeOut(float targetTime)
    {
        UIFade fade = Get<UIFade>(UIType.UIFade);
        if (fade != null)
            fade.FadeOut(true, targetTime);
    }

    public T Add<T>(UIType uiType, bool activeState = false) where T : UIBase
    {
        if (uiList.ContainsKey(uiType))
        {
            uiList[uiType].SetActive(activeState);
            return uiList[uiType] as T;
        }

        T uiBase = this.InstantiateAndCall<T>(path + uiType, "Initialize", transform);
        if (uiBase != null)
        {
            uiList.Add(uiType, uiBase);
            uiBase.SetActive(activeState);
        }
        return uiBase;
    }
}
