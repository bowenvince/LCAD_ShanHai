using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BestiaryTrigger : MonoBehaviour, ITrigger
{
    public int bestiary_set_index;
    public int bestiary_element_index;
    public UnityEvent events;

    public void AddBestiaryLine() 
    {
        BestiarySystem._this.UpdateBestiary(bestiary_set_index, bestiary_element_index, 1);
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
