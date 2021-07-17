/// <summary>
/// Случайное событие - "С днем хрустящего огурчика" 
/// </summary>
public class HappyDayRandomEvent : BaseRandomEvent
{
    public HappyDayRandomEvent()
    {
        _cooldown = 31;
        _probability = 0.20f;
    }

    private const int ENERGY_REFILL=50;
    public override void Activate()
    {
        base.Activate();
        Player.CurrentEnergy += ENERGY_REFILL;
    }
}
