using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

public class DialogSystem : MonoBehaviour
{
    public List<DialogChatSO> dialog_chat;
    public List<DialogBoxSO> dialog_box;
    public List<UnityEvent> do_after_box;

    public bool call_on_enter = false;


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

    public int Get_Current_Dialog_Box_Index()
    {
        for (int i = 0; i < dialog_box.Count; i++) 
        {
            DialogBoxSO dialog = dialog_box[i];
            if (dialog.Check_Condition())
            {
                return i;
            }
        }
        return -1;
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
