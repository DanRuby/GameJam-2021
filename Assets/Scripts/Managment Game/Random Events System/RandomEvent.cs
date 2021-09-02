using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class RandomEvent : ScriptableObject
{
    [SerializeField] public string name;
    [SerializeField,TextArea(3,5)] public string description;
    
    [SerializeField, Range(0.0f,1.0f)] protected float _probability;
    [SerializeField] protected int _cooldown;

    [SerializeField] private PlayerStats changeStats;
    
    [SerializeField] private RandomEventEffect[] eventEffects;

    [SerializeField] private GameEvent[] onActivateGameEvents;
    [SerializeField] private GameEvent[] onDeactivateGameEvents;
    
    protected int _daysBeforeActive;
    
    /// <summary>
    /// Может ли событие произойти
    /// </summary>
    public bool CanHappen => _daysBeforeActive <= 0;

    /// <summary>
    /// Количество дней через которое событие опять может произойти
    /// </summary>
    public int CoolDown { get => _cooldown; }

    /// <summary>
    /// Шанс активации случайного события
    /// </summary>
    public float Probability { get => _probability; }

    /// <summary>
    /// Активировать случайное событие
    /// </summary>
    public virtual void Activate()
    {
        _daysBeforeActive = _cooldown;
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
        

    /// <summary>
    /// Деактивироовать случайно событие
    /// </summary>
    public virtual void Deactivate() {
        foreach (var effect in eventEffects)
        {
            effect.Deactivate();
        }
        
        foreach (var gameEvent in onDeactivateGameEvents)
        {
            gameEvent.Raise();
        }
    }

    /// <summary>
    /// Ежедневное обновление события
    /// </summary>
    public virtual void Tick() => _daysBeforeActive--;
}
