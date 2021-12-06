using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


[CreateAssetMenu(fileName = "new_DialogChoiceNodeSO", menuName = "ScriptableObjects/DialogChoiceNodeSO")]
[Serializable]
public class DialogChoiceNode : DialogNode
{
    [SerializeField]
    private List<Option> m_option_list;

    public List<Option> OptionList => m_option_list;

    public override DialogNode Next_node()
    {
        throw new NotImplementedException();
    }

    [Serializable]
    public class Option 
    {
        [SerializeField]
        private string m_option_text;
        [SerializeField]
        private DialogNode m_dialogNode_next;

        public string OptionText => m_option_text;
        public DialogNode DialogNode_next => m_dialogNode_next;
    }

}

