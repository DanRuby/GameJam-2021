/// <summary>
/// Structure that represents player`s stats
/// </summary>
[System.Serializable]
public struct PlayerStats
{
    public int Money;
    public int Satiety;
    public int Energy;
    public PlayerStats(int money, int satiety, int energy)
    {
        Money = money;
        Satiety = satiety;
        Energy = energy;
    }
    public static PlayerStats operator +(PlayerStats a, PlayerStats b) => new PlayerStats(a.Money + b.Money, a.Satiety + b.Satiety, a.Energy + b.Energy);

    public static PlayerStats operator -(PlayerStats a, PlayerStats b) => new PlayerStats(a.Money - b.Money, a.Satiety - b.Satiety, a.Energy - b.Energy);

    public static PlayerStats operator *(PlayerStats a, float value) => new PlayerStats((int)(a.Money * value), (int)(a.Satiety * value), (int)(a.Energy * value));
    
    public static PlayerStats operator *(PlayerStats a, PlayerStats b) => new PlayerStats((int)(a.Money * b.Money), (int)(a.Satiety * b.Satiety), (int)(a.Energy * b.Energy));

    public static PlayerStats operator -(PlayerStats a) => new PlayerStats(-a.Money, -a.Satiety, -a.Energy);
    
    public bool CheckRequirements()
    {
        if (Player.CurrentMoney < Money)
            return false;
        if (Player.CurrentSatiety < Satiety)
            return false;
        if (Player.CurrentEnergy < Energy)
            return false;
        return true;
    }

    public string GetString()
    {
        string res = string.Empty;
        if (Money != 0)
            res += $"{Money} рублей ";
        if (Satiety != 0)
            res += $"{Satiety} сытости ";
        if (Energy != 0)
            res += $"{Energy} энергии";
        return res;
    }
}
