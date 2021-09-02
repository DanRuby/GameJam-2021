using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class SetEnergyEffect : RandomEventEffect
{
    [SerializeField] private int amountToSet;
    
    public override void Activate()
    {
        Player.CurrentEnergy = amountToSet;
    }
    
    public override void Deactivate()
    {
    }
}
