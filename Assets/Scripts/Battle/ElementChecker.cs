using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ElementChecker : MonoBehaviour, ITrigger
{
    public enum TriggerWay 
    {
        OnCall,
        OnTriggerEnter
    }
    public TriggerWay trigger;
    public string element;
    public UnityEvent to_do;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (trigger == TriggerWay.OnTriggerEnter) 
        {
            OnCall();
        }
    }

    public void OnCall()
    {
        if (TailsmanSystem._this != null) 
        {
            if(TailsmanSystem._this.CheckElement(element))
                to_do.Invoke();
        }
        
    }

    public void OnDestory()
    {
        //nothing happen
    }
}
