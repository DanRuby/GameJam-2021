using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Класс получения апгрейдов 
/// </summary>
[RequireComponent(typeof(Button))]
public class UpgradeActivator : MonoBehaviour
{
    [SerializeField]
    private Upgrade upgrade;

    [SerializeField]
    private int moneyNedded;
    
    Button button;

    public static readonly bool[] ActiveUpgrades = new bool[4];

    void Awake()
    {
        button=GetComponent<Button>();
    }

    public void ActivateUpgrade()
    {
        if (Player.CurrentMoney >= moneyNedded)
        {
            Player.CurrentMoney -= moneyNedded;
            button.interactable = false;
            upgrade.Activate();
        }
    }
}
