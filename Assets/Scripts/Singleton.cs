using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T _this { get; private set; }

    private void Awake()
    {
        if (_this != null && _this != this) 
        {
            Destroy(this.gameObject);
            return;
        }
        _this = this as T;
        //DontDestroyOnLoad(this)
    }
}
