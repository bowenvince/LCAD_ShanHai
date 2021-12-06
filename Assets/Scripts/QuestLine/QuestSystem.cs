using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class QuestSystem : MonoBehaviour
{
    public static QuestSystem _this;

    [SerializeField]
    // tracking current quest state
    public List<Quest> current_quest_state;

    public bool Check_Condition(QuestPathSO pathSO, int state) 
    {
        foreach (Quest quest in current_quest_state)
        {
            if (quest.pathSO == pathSO) 
            {
                if (quest.current_state == state) return true;
                else return false;
            }
        }
        return false;
    }

    public void Update_State(QuestPathSO pathSO, int from_state, int to_state) 
    {
        foreach (Quest quest in current_quest_state)
        {
            if (quest.pathSO == pathSO)
            {
                if (quest.current_state == from_state)
                {
                    quest.current_state = to_state;
                    return;
                }
                else 
                {
                    Debug.Log("ERROR: State is not in " + from_state + " state but" + quest.current_state);
                    return;
                }
                
            }
        }
    }

    public void Start()
    {
        _this = this;
    }
}

//unique Quest Object
[Serializable]
public class Quest 
{
    public QuestPathSO pathSO;
    public int current_state = 0;
}
