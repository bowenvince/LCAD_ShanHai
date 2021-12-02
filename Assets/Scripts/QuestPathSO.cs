using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new_QuestPathSO", menuName = "ScriptableObjects/QuestPathSO")]
[SerializeField]
public class QuestPathSO : ScriptableObject
{
    public string path_name;
}
