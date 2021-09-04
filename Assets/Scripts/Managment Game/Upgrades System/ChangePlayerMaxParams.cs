using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ChangePlayerMaxParams : Upgrade
{
    [SerializeField] private int energy;
    [SerializeField] private int satiety;
    
    public override void Activate()
    {
        Player.MaxEnergy += energy;
        Player.MaxSatiety += satiety;
    }
}
