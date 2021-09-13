using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ChangeSleepParamsUpgrade : Upgrade
{
    [SerializeField] private int hourToEnergyMul;
    [SerializeField] private int energyRefill;
    
    public override void Activate()
    {
        Week.MAX_ENERGY_REFILL += energyRefill;
        Week.HOURS_TO_ENERGY_MULTIPLIER += hourToEnergyMul;
    }
}
