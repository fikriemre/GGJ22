using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventTriggerInformer : Informer
{
    public UnityEvent OnEventTrigger;
    private bool eventSaved = false;
    public void SaveTriggerEvent()
    {
        eventSaved = true;
    }
    public override void SetActivation(bool status)
    {
        base.SetActivation(status);
        if(eventSaved)
        {
            OnEventTrigger?.Invoke();
            eventSaved = false;
        }
    }
}