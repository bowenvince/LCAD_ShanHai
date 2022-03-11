using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public QuestPathSO quest_path;

    [SerializeField]
    public List<SceneSet> SceneSetList;

    public string default_sceneToLoad;
    public int default_destination;

    public bool facing_right = true;

    

    [System.Serializable]
    public class SceneSet
    {
        public int valid_state;
        public string sceneToLoad;
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
            if (QuestSystem._this.Check_Condition(quest_path, SceneSetList[i].valid_state))
            {
                SceneTransitionHandler._this.SceneTransition(SceneSetList[i].sceneToLoad, i, facing_right);
                return;
            }
        }
        if(SceneManager.GetSceneByName(default_sceneToLoad) != null)
            SceneTransitionHandler._this.SceneTransition(default_sceneToLoad, default_destination, facing_right);
    }
}

