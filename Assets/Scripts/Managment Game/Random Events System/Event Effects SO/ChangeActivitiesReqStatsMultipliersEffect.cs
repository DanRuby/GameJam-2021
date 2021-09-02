using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO
[CreateAssetMenu()]
public class ChangeActivitiesReqStatsMultipliersEffect : RandomEventEffect
{
    [SerializeField] private PlayerStats multipliers;
    
    public override void Activate()
    {
        ActivityButton.ChangeRequiredStatsMultiplier(multipliers);
    }
    
    public override void Deactivate()
    {
        ActivityButton.ChangeRequiredStatsMultiplier(-multipliers);
    }
}
