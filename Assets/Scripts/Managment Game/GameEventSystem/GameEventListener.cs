using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Invokes a UnityEvent when a 'GameEvent' is raised  
/// </summary>
public class GameEventListener : MonoBehaviour
{
    [SerializeField] private GameEvent gameEvent;
    [SerializeField] private UnityEvent response;
    
    private void Awake()
    {
        gameEvent.Subscribe(this);
    }

    public void Raise()
    {
        response.Invoke();
    }
}
