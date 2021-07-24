/// <summary>
/// ��������� ������� - "�����������" 
/// </summary>
public class IllnessRandomEvent : BaseRandomEvent
{
    public IllnessRandomEvent()
    {
        _cooldown = 14;
        _probability = 0.05f;
    }

    private int MONEY_SPENT = 500;
    private const float ENERGY_CHANGE = .25F;

    public static event System.Action<float> IllnesEventFired;
    public static event System.Action<float> IllnesEventEnded;
    
    public override void Activate()
    {
        base.Activate();
        Player.CurrentMoney -= MONEY_SPENT;
        IllnesEventFired?.Invoke(ENERGY_CHANGE);
    }
    public override void Deactivate() => IllnesEventEnded?.Invoke(ENERGY_CHANGE);
}
