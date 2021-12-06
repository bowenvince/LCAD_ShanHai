using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


//[CreateAssetMenu(fileName = "new_DialogNodeSO", menuName = "ScriptableObjects/DialogNodeSO")]
[Serializable]
public abstract class DialogNode : ScriptableObject
{
    [SerializeField]
    private NarrationLine m_narrationLine;

    public NarrationLine NarrationLine_current => m_narrationLine;

    public abstract DialogNode Next_node();

}
