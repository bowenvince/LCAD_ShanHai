using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{

    [SerializeField]
    public List<SceneSet> SceneSetList;

    public string default_sceneToLoad;
    public int default_destination;
    public bool default_HUB_display = true;

    public bool facing_right = true;

    

    [System.Serializable]
    public class SceneSet
    {
        public QuestPathSO quest_path;
        public int valid_state;
        public string sceneToLoad;
        public int destination;
        public bool HUD_display = true;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger)
        {
            other.GetComponent<PlayerMovement>().enabled = false;
            MakeTransition();
        }
    }

    public void MakeTransition() 
    {
        for (int i = 0; i < SceneSetList.Count; i++)
        {
            if (QuestSystem._this.Check_Condition(SceneSetList[i].quest_path, SceneSetList[i].valid_state))
            {
                GameManager._this.EnableHUD(SceneSetList[i].HUD_display);
                SceneTransitionHandler._this.SceneTransition(SceneSetList[i].sceneToLoad, SceneSetList[i].destination, facing_right);
                return;
            }
        }
        if (SceneManager.GetSceneByName(default_sceneToLoad) != null) 
        {
            GameManager._this.EnableHUD(default_HUB_display);
            SceneTransitionHandler._this.SceneTransition(default_sceneToLoad, default_destination, facing_right);
        }
            
    }
}

