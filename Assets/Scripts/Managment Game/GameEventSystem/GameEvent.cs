using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameEvent : ScriptableObject
{
    private List<GameEventListener> eventListeners=new List<GameEventListener>();
    
    public void Raise()
    {
        foreach (var listener in eventListeners)
        {
            listener.Raise();
        }
    }

    public void Subscribe(GameEventListener eventListener)
    {
        eventListeners.Add(eventListener);
    }
}
