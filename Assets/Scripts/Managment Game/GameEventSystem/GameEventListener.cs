using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventListener : MonoBehaviour
{
    [SerializeField] private GameEvent gameEvent;

    private void Awake()
    {
        gameEvent.Subscribe(this);
    }

    public void Raise()
    {
        
    }
}
