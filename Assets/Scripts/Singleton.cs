using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T _this { get; private set; }

    private void Awake()
    {
        if (_this != null) 
        {
            Destroy(this);
            return;
        }
        _this = this as T;
        //DontDestroyOnLoad(this)
    }
}
