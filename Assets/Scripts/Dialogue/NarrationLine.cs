using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "new_NarrationLineSO", menuName = "ScriptableObjects/NarrationLineSO")]
[Serializable]
public class NarrationLine : ScriptableObject
{
    [SerializeField]
    private NarrationCharacter m_character;
    [SerializeField]
    private string m_text;

    public NarrationCharacter Character => m_character;
    public string Text => m_text;
}
