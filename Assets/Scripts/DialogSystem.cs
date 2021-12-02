using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class DialogSystem : MonoBehaviour
{
    public List<DialogSO> dialog;

    // check if any dialog match current condition, return it (condition must be exclusive!)
    public DialogSO Get_Current_Dialog() 
    {
        foreach (DialogSO dialog in dialog) 
        {
            if (dialog.Check_Condition()) 
            {
                return dialog;
            }
        }
        return null;
    }
}

[CreateAssetMenu(fileName = "new_DialogSO", menuName = "ScriptableObjects/DialogSO")]
[Serializable]
public class DialogSO : ScriptableObject
{
    public List<Dialog_Condition> condition_list;
    public string text;
    public List<Dialog_Solution> solution_list;

    // all condition must meet
    public bool Check_Condition() 
    {
        foreach (Dialog_Condition condition in condition_list) 
        {
            if (!condition.Check_Condition()) return false;
        }
        return true;
    }
    // process all solution
    public void Process_Solution()
    {
        foreach (Dialog_Solution solution in solution_list)
        {
            solution.Process_Solution();
        }
    }
}

[Serializable]
public class Dialog_Condition 
{
    public QuestPathSO pathSO;
    public int progress_state;

    public bool Check_Condition() 
    {
        return QuestSystem._this.Check_Condition(pathSO, progress_state);
    }
}

[Serializable]
public class Dialog_Solution
{
    public QuestPathSO pathSO;
    public int from_state;
    public int to_state;

    public void Process_Solution() 
    {
        QuestSystem._this.Update_State(pathSO,from_state,to_state);
    }
}
