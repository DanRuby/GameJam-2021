
/// <summary>
/// ������� ����� ��������� �������
/// </summary>
public abstract class BaseRandomEvent 
{
    /// <summary>
    /// ����� �� ������� ���������
    /// </summary>
    public bool CanHappen => _daysBeforeActive <= 0;

    /// <summary>
    /// ���������� ���� ����� ������� ������� ����� ����� ���������
    /// </summary>
    public int CoolDown { get => _cooldown; }

    /// <summary>
    /// ���� ��������� ���������� �������
    /// </summary>
    public float Probability { get => _probability; }

    protected float _probability;
    protected int _cooldown;
    protected int _daysBeforeActive;

    /// <summary>
    /// ������������ ��������� �������
    /// </summary>
    public virtual void Activate() => _daysBeforeActive = _cooldown;

    /// <summary>
    /// ��������������� �������� �������
    /// </summary>
    public virtual void Deactivate() { return; }

    /// <summary>
    /// ���������� ���������� �������
    /// </summary>
    public virtual void Tick() => _daysBeforeActive--;
}
