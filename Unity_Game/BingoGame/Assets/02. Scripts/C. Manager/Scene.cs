using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Scene : MonoBehaviour
{
    public abstract void Enter();

    public abstract void Exit();

    public abstract void Progress(float progress);
}
