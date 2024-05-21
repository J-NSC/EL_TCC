using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class SignalListener : MonoBehaviour
{
    public Signal signal;
    public UnityEvent signalEvent;
    
    public void OnSignalRaised()
    {
        signalEvent.Invoke();
    }

    void OnEnable()
    {
        signal.RegisterListener(this);
    }

    void OnDisable()
    {
        signal.DeResgisterListener(this);
    }
}
