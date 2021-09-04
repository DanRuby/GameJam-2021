using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ChangeActivityMulUpgrade : Upgrade
{
    [SerializeField] private float energyMul;
    [SerializeField] private float moneyMul;
    [SerializeField] private float satietyMul;
    
    public override void Activate()
    {
        Activity.ChangeRequirmentsMultipliers(energyMul,moneyMul,satietyMul);
    }
}
