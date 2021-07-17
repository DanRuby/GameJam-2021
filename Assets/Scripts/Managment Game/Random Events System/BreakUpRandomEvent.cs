/// <summary>
/// Расставание
/// </summary>
public class BreakUpRandomEvent : BaseRandomEvent
{
    public BreakUpRandomEvent()
    { 
        _cooldown = 180;
        _probability = 0.09f;
    }
    public override void Activate()
    {
        base.Activate();
        Player.CurrentEnergy = 0;
    }
}