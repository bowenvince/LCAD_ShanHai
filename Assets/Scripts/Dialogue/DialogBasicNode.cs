using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "new_DialogBasicNodeSO", menuName = "ScriptableObjects/DialogBasicNodeSO")]
[Serializable]
public class DialogBasicNode : DialogNode
{
    [SerializeField]
    private DialogNode m_dialogNode_next;

    public DialogNode DialogNode_next => m_dialogNode_next;

    public override DialogNode Next_node()
    {
        return m_dialogNode_next;
    }
}
