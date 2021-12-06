using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class DialogSystem : MonoBehaviour
{
    public List<DialogChatSO> dialog_chat;
    public List<DialogBoxSO> dialog_box;


    // check if any dialog match current condition, return a random one from all valid
    public DialogChatSO Get_Current_Dialog_Chat() 
    {
        List<DialogChatSO> dialog_list = new List<DialogChatSO>();
        foreach (DialogChatSO dialog in dialog_chat) 
        {
            if (dialog.Check_Condition()) 
            {
                dialog_list.Add(dialog);
            }
        }
        if (dialog_list.Count != 0) 
        {
            return dialog_list[UnityEngine.Random.Range(0, dialog_list.Count)];
        }
        return null;
    }

    // check if any dialog match current condition, return it (condition must be exclusive!)
    public DialogBoxSO Get_Current_Dialog_Box()
    {
        foreach (DialogBoxSO dialog in dialog_box)
        {
            if (dialog.Check_Condition())
            {
                return dialog;
            }
        }
        return null;
    }
}

[CreateAssetMenu(fileName = "new_DialogChatSO", menuName = "ScriptableObjects/DialogChatSO")]
[Serializable]
public class DialogChatSO : ScriptableObject
{
    [SerializeField]
    private List<Dialog_Condition> condition_list;
    [SerializeField]
    private string m_text;
    [SerializeField]
    private List<Dialog_Solution> solution_list;

    public string text => m_text;

    // all condition must meet
    public bool Check_Condition() 
    {
        if (condition_list.Count == 0) return true;
        foreach (Dialog_Condition condition in condition_list) 
        {
            if (!condition.Check_Condition()) return false;
        }
        return true;
    }
    // process all solution
    public void Process_Solution()
    {
        if (solution_list.Count == 0) return;
        foreach (Dialog_Solution solution in solution_list)
        {
            solution.Process_Solution();
        }
    }
}

[CreateAssetMenu(fileName = "new_DialogBoxSO", menuName = "ScriptableObjects/DialogBoxSO")]
[Serializable]
public class DialogBoxSO : ScriptableObject
{
    [SerializeField]
    private List<Dialog_Condition> condition_list;
    [SerializeField]
    private DialogNode m_first_node;
    public DialogNode first_node => m_first_node;
    [SerializeField]
    private List<Dialog_Solution> solution_list;

    // all condition must meet
    public bool Check_Condition()
    {
        if (condition_list.Count == 0) return true;
        foreach (Dialog_Condition condition in condition_list)
        {
            if (!condition.Check_Condition()) return false;
        }
        return true;
    }
    // process all solution
    public void Process_Solution()
    {
        if (solution_list.Count == 0) return;
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
