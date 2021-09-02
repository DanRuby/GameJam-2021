п»їusing UnityEngine;

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

