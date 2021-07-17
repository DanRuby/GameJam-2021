
/// <summary>
/// Базовый класс сучайного события
/// </summary>
public abstract class BaseRandomEvent 
{
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

    protected float _probability;
    protected int _cooldown;
    protected int _daysBeforeActive;

    /// <summary>
    /// Активировать случайное событие
    /// </summary>
    public virtual void Activate() => _daysBeforeActive = _cooldown;

    /// <summary>
    /// Деактивироовать случайно событие
    /// </summary>
    public virtual void Deactivate() { return; }

    /// <summary>
    /// Ежедневное обновление события
    /// </summary>
    public virtual void Tick() => _daysBeforeActive--;
}
