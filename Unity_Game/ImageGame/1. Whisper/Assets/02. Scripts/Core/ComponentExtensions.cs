using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using System.Xml.Linq;

public static class ComponentExtensions
{
    public static T Find<T>(this Component component,string path) where T : Component
    {
        Transform findTransform = component.transform.Find(path);

        if( findTransform != null )
        {
            return findTransform.GetComponent<T>();
        }
        return null;
    }

    public static T FindAndCall<T>(this Component component, string path, string function, System.Object[] objects = null) where T : Component
    {
        T t = component.Find<T>(path);

        if (t != null)
            return t;

        Invoke<T>(t,function, objects);

        return t;

    } 

    public static void SetEnable( this Behaviour component, bool enable)
    {
        component.enabled = enable;
    }

    public static void SetActive(this Component component, bool active)
    {
        component.gameObject.SetActive(active);
    }

    // 확장함수 시작
    public static T CreateObject<T>(this Component component, Transform parent = null) where T : Component
    {
        GameObject obj = new GameObject(typeof(T).Name, typeof(T));

        obj.transform.SetParent(parent);
        return obj.GetComponent<T>();
    }

    public static T CreateObjectAndCall<T>(this Component component, string function, Transform parent = null, System.Object[] objects = null) where T : Component
    {
        T t = CreateObject<T>(parent);
        Invoke<T>(t,function, objects);
        return t;
    }

    // 확장함수 끝
    // 정적함수 시작
    public static T CreateObject<T>(Transform parent = null) where T : Component
    {
        GameObject obj = new GameObject(typeof(T).Name, typeof(T));

        obj.transform.SetParent(parent);
        return obj.GetComponent<T>();
    }

    private static T CreateObjectAndCall<T>(string function, Transform parent = null, System.Object[] objects = null) where T : Component
    {
        T t = CreateObject<T>(parent);

        Invoke<T>(t, function, objects);

        return t;
    }

    public static T Instantiate<T>(this Component component, string path) where T : Component
    {
        T t = Resources.Load<T>(path);

        if (t == null) return null;
        T newComponent = UnityEngine.Object.Instantiate(t);
        newComponent.name = t.name;

        return newComponent;
    }
    
    public static T Instantiate<T>(this Component component, string path, Transform parent) where T : Component
    {
        T newComponent = Instantiate<T>(component, path);
        if (newComponent == null) return null;
        newComponent.transform.SetParent(parent);

        return newComponent;
    }

    public static T Instantiate<T>(this Component component, string path, Vector3 position, Transform parent = null) where T : Component
    {
        T t = component.Instantiate<T>(path);
        if(t == null) return null;

        t.transform.position = position;
        t.transform.rotation = Quaternion.identity;
        t.transform.SetParent(parent);
        return t;
    }

    public static T Instantiate<T>(this Component component, string path, Vector3 position, Quaternion rotation, Transform parent = null) where T : Component
    {
        T t = component.Instantiate<T>(path);
        if (t == null) return null;

        t.transform.position = position;
        t.transform.rotation = rotation;
        t.transform.SetParent(parent);
        return t;
    }

    public static void Invoke<T>(Component component, string function, System.Object[] objects = null)
    {
        Type tType = typeof(T);
        MethodInfo methodInfo = tType.GetMethod(function);
        methodInfo.Invoke(component, objects);
    }
    public static T InstantiateAndCall<T>(this Component component, string path, string function, System.Object[] objects = null) where T : Component
    {

        T newComponent = Instantiate<T>(component, path);
        if (newComponent == null) return null;
        Invoke<T>(newComponent, function, objects);

        return newComponent;
    }
    public static T InstantiateAndCall<T>(this Component component, string path, string function, Transform parent, System.Object[] objects = null) where T : Component
    {

        T newComponent = Instantiate<T>(component, path, parent);
        if (newComponent == null) return null;
        Invoke<T>(newComponent, function, objects);

        return newComponent;
    }


    public static T InstantiateAndCall<T>(this Component component, string path, string function,
                                           Vector3 position, Transform parent = null, System.Object[] objects = null) where T : Component
    {

        T newComponent = Instantiate<T>(component, path, position, parent);
        if (newComponent == null) return null;
        Invoke<T>(newComponent, function, objects);

        return newComponent;
    }

    public static T InstantiateAndCall<T>(this Component component, string path, string function,
                                           Vector3 position, Quaternion rotation, Transform parent = null, System.Object[] objects = null) where T : Component
    {

        T newComponent = Instantiate<T>(component, path, position, parent);
        if (newComponent == null) return null;
        Invoke<T>(newComponent, function, objects);

        return newComponent;
    }


}
