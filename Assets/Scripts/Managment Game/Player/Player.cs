using System;

/// <summary>
/// Class that stores player`s stats 
/// </summary>
public static class Player 
{
    private const int START_MONEY = 1500;
    private const float DEBUFF_SATIETY_MULTYPLIER=.5f;

    #region �����
    
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
                
                //Account debuff from small satiety levels
                if (currentSatiety < DebuffSatietyValue && prevSatiety>=DebuffSatietyValue)
                    Activity.ChangeBenefitsMultipliers(-DEBUFF_SATIETY_MULTYPLIER,0,0);
                else if(currentSatiety >= DebuffSatietyValue && prevSatiety<DebuffSatietyValue)
                    Activity.ChangeBenefitsMultipliers(DEBUFF_SATIETY_MULTYPLIER,0,0);
            }
        }
    }
    private static int currentSatiety=50;
    
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
    
    public const int DebuffSatietyValue=30;
    
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

    #region StatsChangedEvents
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
