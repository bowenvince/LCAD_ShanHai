using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "new_NarrationCharacterSO", menuName = "ScriptableObjects/NarrationCharacterSO")]
[Serializable]
public class NarrationCharacter : ScriptableObject
{
    [SerializeField]
    private string m_name;
    [SerializeField]
    private Sprite m_sprite;
    [SerializeField]
    private bool m_is_player = false;

    public string Name => m_name;
    public Sprite Sprite => m_sprite;

    public bool is_player => m_is_player;
}
