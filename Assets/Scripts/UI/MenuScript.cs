using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    public string scene_name;

    public void Start_Game() 
    {
        SceneTransitionHandler._this.SceneTransition(scene_name, 0, true);
    }
}
