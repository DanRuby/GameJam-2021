using UnityEngine;
using UnityEngine.UI;

public enum Upgrade
{
    Floppa, Matrace, Monitor, Gym
}

[RequireComponent(typeof(Button))]
public class UpgradeActivator : MonoBehaviour
{
    [SerializeField]
    private Upgrade upgrade;

    [SerializeField]
    private int moneyNedded;
    Button button;

    public static bool[] ActiveUpgrades = new bool[4];

    void Awake()
    {
        button=GetComponent<Button>();
        if (ActiveUpgrades[(int)upgrade] == true)
            button.interactable = false;
        else button.interactable = true;
    }

    public void ActivateUpgrade()
    {
        if (Player.CurrentMoney >= moneyNedded)
        {
            Player.CurrentMoney -= moneyNedded;
            button.interactable = false;
            ActiveUpgrades[(int)upgrade] = true;
            if (upgrade == Upgrade.Floppa)
                Player.MaxEnergy += 30;
        }
    }

    public static void RecalculateRequiredStats(ref PlayerStats stats)
    {
        if (ActiveUpgrades[(int)Upgrade.Monitor])
            stats.Energy /= 2;
        if (ActiveUpgrades[(int)Upgrade.Gym])
            stats.Satiety /= 2;
    }
}
