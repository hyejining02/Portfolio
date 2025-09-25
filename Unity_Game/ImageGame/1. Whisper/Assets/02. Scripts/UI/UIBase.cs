using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBase : MonoBehaviour
{

    public virtual void Initialize() { }

    public void SetPosition(Vector3 position) 
    {
        transform.position = position;
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    public virtual void SetEnable(bool enable)
    {
        enabled = enable;
    }

    public virtual void Close()
    {
        SetActive(false);
    }

}
