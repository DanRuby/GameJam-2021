/// <summary>
/// Пожарная тренировка
/// </summary>
public class FireTrainingRandomEvent : BaseRandomEvent
{
    public FireTrainingRandomEvent()
    {
        _probability = 0.10f;
        _cooldown = 180;
    }
    public override void Activate()
    {
        base.Activate();
        Player.CurrentEnergy -= 15;
        Week.HoursLeft -= 2;
    }
}
