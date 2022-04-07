using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestlineEnableGO : MonoBehaviour
{
    #region Helper Class
    [System.Serializable]
    public class QuestlineEnable
    {
        public QuestPathSO pathSO;
        public int disable_state;
    }
    #endregion

    public List<QuestlineEnable> questlineValid;

    private void OnEnable()
    {
        if (QuestSystem._this == null) { return; }

        foreach (QuestlineEnable quest in questlineValid) 
        {
            if (QuestSystem._this.Check_Condition(quest.pathSO, quest.disable_state)) 
            {
                return;
            }
        }
        this.gameObject.SetActive(false);
    }
}
