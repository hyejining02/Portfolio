using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TSingleton<T> : MonoBehaviour where T : TSingleton<T>
{
    private static T instance;

    protected TSingleton() { }

    public virtual void Initialize() { }

    public static T Instance
    {
        get
        {
            if ( instance == null )
            {
                instance = ComponentExtensions.CreateObject<T>();
                instance.Initialize();
                DontDestroyOnLoad(instance.gameObject);
            }

            return instance;
        }
    }

}
