using System;

/// <summary>
/// ���������� ������
/// </summary>
public static class Player 
{
    /*��������� �������� ����*/
    private const int START_MONEY = 1500;
    private const int DEBUFF_SATIETY_MULTYPLIER=2;

    #region �����

    /// <summary>
    /// ������� ������
    /// </summary>
    public static int CurrentMoney 
    {
        get => currentMoney;
        set 
        {
            if (currentMoney != value)
            {
                currentMoney = Math.Min(Math.Max(value, 0), int.MaxValue);
                MoneyValueChanged?.Invoke();
            }
        }
    }
    private static int currentMoney=START_MONEY;

    /// <summary>
    /// ������� ��������
    /// </summary>
    public static int CurrentSatiety 
    {
        get => currentSatiety;
        set
        {
            if (currentSatiety != value)
            {
                currentSatiety = Math.Min(Math.Max(value,0),maxSatiety);
                SatietyValueChanged?.Invoke();
            }
        }
    }
    private static int currentSatiety=50;

    /// <summary>
    /// ������� �������
    /// </summary>
    public static int CurrentEnergy
    {
        get => currentEnergy;
        set 
        {
            if (currentEnergy != value)
            {
                currentEnergy = Math.Min(Math.Max(value, 0), maxEnergy);
                EnergyValueChanged?.Invoke();
            }
        }
    }
    private static int currentEnergy = 50;

    /// <summary>
    /// ����������� ��������� �������� ��������
    /// </summary>
    public static int MaxSatiety
    { 
        get => maxSatiety;
        set
        {
            if (maxSatiety != value)
            {
                maxSatiety = value;
                SatietyValueChanged?.Invoke();
            }
        }
    }
    private static int maxSatiety = 100;

    /// <summary>
    /// ��������, ���� �������� ������� ����������������� �� ��� ����������
    /// </summary>
    public const int DebuffSatietyValue=30;

    /// <summary>
    /// ����������� ��������� �������� �������
    /// </summary>
    public static int MaxEnergy
    {
        get => maxEnergy;
        set
        {
            if (maxEnergy != value)
            {
                maxEnergy = value;
                EnergyValueChanged?.Invoke();
            }
        }
    }
    private static int maxEnergy = 100;
    #endregion

    #region ������� ��������� ������
    public static event Action MoneyValueChanged;
    public static event Action SatietyValueChanged;
    public static event Action EnergyValueChanged;
    #endregion

    public static void ChangeStats(PlayerStats change)
    {
        CurrentEnergy += change.Energy;
        CurrentMoney += change.Money;
        CurrentSatiety += change.Satiety;
    }

    public static PlayerStats AccountSatietyDebuff(PlayerStats initial)
    {
        if (CurrentSatiety < DebuffSatietyValue)
            initial.Energy /= DEBUFF_SATIETY_MULTYPLIER;
        return initial;
    }
}
