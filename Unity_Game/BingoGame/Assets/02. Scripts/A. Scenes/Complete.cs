using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Complete : Scene
{
    void Start()
    {

    }

    public override void Enter()
    {
        UIManager.Instance.FadeIn(1f);
    }

    public override void Exit()
    {

    }

    public override void Progress(float progress)
    {

    }
}
