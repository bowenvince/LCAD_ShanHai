using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagerLocal : MonoBehaviour
{
    public static SceneManagerLocal _this;

    public GameObject player;

    public List<GameObject> position;

    private void Awake()
    {
        _this = this;
    }
}
