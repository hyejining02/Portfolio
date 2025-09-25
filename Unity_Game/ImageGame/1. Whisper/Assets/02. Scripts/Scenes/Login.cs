using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Login : Scene
{
    public override void Enter()
    {
        UIManager.Instance.FadeIn(2);
    }

    public override void Exit()
    {
       
    }

    public override void Progress(float progress)
    {
        
    }

}
