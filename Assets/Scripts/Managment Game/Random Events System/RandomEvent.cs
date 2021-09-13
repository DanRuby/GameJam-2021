using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu()]
public class RandomEvent : ScriptableObject
{
    [SerializeField] public string Name;
    [SerializeField,TextArea(3,5)] public string Description;
    
    [Tooltip("Odds of event occuring. Bigger the number, bigger the odds")]
    [SerializeField, Range(0.0f,1.0f)] private float probability;
    [SerializeField] private int cooldown;

    [SerializeField] private PlayerStats changeStats;
    [SerializeField] private RandomEventEffect[] eventEffects;

    [SerializeField] private GameEvent[] onActivateGameEvents;
    [SerializeField] private GameEvent[] onDeactivateGameEvents;
    
    private int daysBeforeActive;
    
    public bool CanOccur => daysBeforeActive <= 0;
    public float Probability => probability;
    
    public void Activate()
    {
        daysBeforeActive = cooldown;
        Player.ChangeStats(changeStats);
        
        foreach (var effect in eventEffects)
        {
            effect.Activate();
        }

        foreach (var gameEvent in onActivateGameEvents)
        {
            gameEvent.Raise();
        }
    }
    
    public void Deactivate() {
        foreach (var effect in eventEffects)
        {
            effect.Deactivate();
        }
        
        foreach (var gameEvent in onDeactivateGameEvents)
        {
            gameEvent.Raise();
        }
    }
    
    public void Tick() => daysBeforeActive--;
}
