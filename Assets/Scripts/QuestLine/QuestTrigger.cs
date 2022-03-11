using UnityEngine;
using UnityEngine.Events;

public class QuestTrigger : MonoBehaviour, ITrigger
{
    public UnityEvent events;
    public QuestPathSO pathSO;
    public int from;
    public int to;

    public void UpdateQuest() 
    {
        QuestSystem._this.Update_State(pathSO, from, to);
    }

    public void OnCall()
    {
        events.Invoke();
    }

    public void OnDestory()
    {
        Destroy(this.gameObject);
    }
}
