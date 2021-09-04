using System;

/// <summary>
/// пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅ
/// </summary>
public static class Player 
{
    /*пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅ*/
    private const int START_MONEY = 1500;
    private const float DEBUFF_SATIETY_MULTYPLIER=.5f;

    #region пїЅпїЅпїЅпїЅпїЅ

    /// <summary>
    /// пїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅ
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
    /// пїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ
    /// </summary>
    public static int CurrentSatiety 
    {
        get => currentSatiety;
        set
        {
            if (currentSatiety != value)
            {
                int prevSatiety = currentSatiety;
                currentSatiety = Math.Min(Math.Max(value,0),maxSatiety);
                SatietyValueChanged?.Invoke();
                
                if (currentSatiety < DebuffSatietyValue && prevSatiety>=DebuffSatietyValue)
                    Activity.ChangeBenefitsMultipliers(-DEBUFF_SATIETY_MULTYPLIER,0,0);
                else if(currentSatiety >= DebuffSatietyValue && prevSatiety<DebuffSatietyValue)
                    Activity.ChangeBenefitsMultipliers(DEBUFF_SATIETY_MULTYPLIER,0,0);
            }
        }
    }
    private static int currentSatiety=50;

    /// <summary>
    /// пїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅ
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
    /// пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ
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
    /// пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ, пїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅ пїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ
    /// </summary>
    public const int DebuffSatietyValue=30;

    /// <summary>
    /// пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅ
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

    #region пїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅ
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
}
