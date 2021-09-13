using UnityEngine;

/// <summary>
/// Random event effect that changes the amount of hours in a day
/// </summary>
[CreateAssetMenu()]
public class ChangeTimeEffect : RandomEventEffect
{
    [SerializeField] private int hoursChange;
    
    public override void Activate()
    {
        Week.HoursLeft += hoursChange;
    }
    public override void Deactivate()
    {
    }
}

