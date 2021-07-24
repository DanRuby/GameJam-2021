/// <summary>
/// ��������� ������� - "������� ����������" 
/// </summary>
public class FoodPoisoningRandomEvent : BaseRandomEvent
{
    public FoodPoisoningRandomEvent()
    {
        _probability = 0.15f;
        _cooldown = 31;
    }

    private const float SATIETY_CHANGE = .25f;
    private const int MONEY = 500;
    
    public static event System.Action<float> FoodPoisoningEventFired;
    public static event System.Action<float> FoodPoisoningEventEnded;
    public override void Activate()
    {
        base.Activate();
        Player.CurrentMoney -= MONEY;
        FoodPoisoningEventFired?.Invoke(SATIETY_CHANGE);
    }
    public override void Deactivate() => FoodPoisoningEventEnded?.Invoke(SATIETY_CHANGE);
}
