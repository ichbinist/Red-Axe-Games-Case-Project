using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Object
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<T>();
            }
            return instance;
        }
    }

    protected virtual void Awake()
    {
        SetInstance();
    }

    public static bool Exists()
    {
        return instance != null;
    }

    public bool SetInstance()
    {
        if(instance != null && instance != gameObject.GetComponent<T>())
        {
            return false;
        }
        instance = gameObject.GetComponent<T>();
        return true;
    }
}
