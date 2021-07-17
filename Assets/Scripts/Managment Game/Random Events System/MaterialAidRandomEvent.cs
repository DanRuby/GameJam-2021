/// <summary>
/// Случайное событие - "Материальная помощь" 
/// </summary>
public class MaterialAidRandomEvent : BaseRandomEvent
{
    public MaterialAidRandomEvent()
    {
        _cooldown = 180;
        _probability = 0.25f;
    }

    private int MONEY_GIVEN = 2500;

    public override void Activate()
    {
        base.Activate();
        Player.CurrentMoney += MONEY_GIVEN;
    }

}
