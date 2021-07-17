/// <summary>
/// Тараканы
/// </summary>
public class CockRoachesRandomEvent : BaseRandomEvent
{
    private const int MONEY_CHANGE = 300;
    public CockRoachesRandomEvent()
    {
        _probability = 0.15f;
        _cooldown = 180;
    }
    public override void Activate()
    {
        base.Activate();
        Player.CurrentMoney-= MONEY_CHANGE;
    }
}