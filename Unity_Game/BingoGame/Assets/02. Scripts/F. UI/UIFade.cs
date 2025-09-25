using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIFade : UIBase
{
    private float elapsed = 0;
    private bool update = false;
    private Image image;

    public override void Initialize()
    {
        //image = GetComponentInChildren<Image>();
        image = this.Find<Image>("Image");
    }

    private void Fade(Color start, Color end, bool afterState = false, float targetTime = 1)
    {
        SetActive(true);

        if (update) return;
        image.color = start;
        StartCoroutine(IEFade(start, end, afterState, targetTime));
    }

    private IEnumerator IEFade(Color start, Color end, bool afterState = false, float targetTime = 1)
    {
        update = true;
        elapsed = 0;
        while (update)
        {
            elapsed += Time.deltaTime / targetTime;
            image.color = Color.Lerp(start, end, elapsed);

            if (elapsed >= 1.0f)
            {
                SetActive(afterState);
                update = false;
            }
            yield return null;
        }
    }

    public void FadeIn(bool afterState, float targetTime = 1)
    {
        Fade(end: new Color(0, 0, 0, 0), start: Color.black, afterState: afterState, targetTime: targetTime);
    }

    public void FadeOut(bool afterState, float targetTime = 1)
    {
        Fade(end: Color.black, start: new Color(0, 0, 0, 0), afterState: afterState, targetTime: targetTime);
    }
}
