﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
