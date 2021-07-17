using System;

/// <summary>
/// Абстракция игрока
/// </summary>
public static class Player 
{
    /*Начальные значения игры*/
    private const int START_MONEY = 1500;
    private const int DEBUFF_SATIETY_MULTYPLIER=2;

    #region Статы

    /// <summary>
    /// Текущие деньги
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
    /// Текущая сытность
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
    /// Текущая энергия
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
    /// Максимально возможное значение сытности
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
    /// Значение, ниже которого энергия восстанавливается не так эффективно
    /// </summary>
    public const int DebuffSatietyValue=30;

    /// <summary>
    /// Максимально возможное значение энергии
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

    #region События изменений статов
    public static Action MoneyValueChanged;
    public static Action SatietyValueChanged;
    public static Action EnergyValueChanged;
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
