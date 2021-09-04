using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ChangeActivitiesReqStatsMultipliersEffect : RandomEventEffect
{
    [SerializeField] private float satietyMulChange;
    [SerializeField] private float energyMulChange;
    [SerializeField] private float moneyMulChange;
    
    public override void Activate()
    {
        Activity.ChangeRequirmentsMultipliers(energyMulChange,moneyMulChange,satietyMulChange);
    }
    
    public override void Deactivate()
    {
        Activity.ChangeRequirmentsMultipliers(-energyMulChange,-moneyMulChange,-satietyMulChange);
    }
}
