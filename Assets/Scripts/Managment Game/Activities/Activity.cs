using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

[CreateAssetMenu()]
public class Activity : ScriptableObject
{
    [SerializeField] public string Name;
    [SerializeField, TextArea(3,5)] public string description;
    [SerializeField] private bool oneUsePerDay;
    [SerializeField] private bool weekdayOnly;
    [SerializeField] private PlayerStats requirements;
    [SerializeField] private int timeRequired;
    
    [SerializeField] private PlayerStats baseBenefits;
    [SerializeField] private PlayerStats randomBenefits;
    [SerializeField] private LabStats baseLabBenefits;
    [SerializeField] private LabStats randomLabBenefits;

    public event System.Action<bool> OnActivityStateChanged;
    public static event System.Action<PlayerStats, LabStats> ActivityUsed;
    
    private bool canBePerformed;
    private bool wasUsedToday;
    private int numOfBlockingEffects;
    private bool isBlocked => numOfBlockingEffects>0;


    private static float moneyMul=1.0f;
    private static float satietyMul=1.0f;
    private static float energyMul=1.0f;

    
    public void PerformActivity()
    {
        wasUsedToday = true;

        float randomness = Random.value;
        PlayerStats randomAffectPlayer = randomBenefits * randomness;
        PlayerStats totalPlayerBenefits = baseBenefits + randomAffectPlayer;
        totalPlayerBenefits = Player.AccountSatietyDebuff(totalPlayerBenefits);
        Player.ChangeStats(totalPlayerBenefits - AccountMultipliers() );

        LabStats randomAffectLab = randomLabBenefits * randomness;
        LabStats totalLabAffect = baseLabBenefits + randomAffectLab;
        LabWork.ChangeValues(totalLabAffect);

        Week.HoursLeft -= timeRequired;
        if (oneUsePerDay)
            OnActivityStateChanged?.Invoke(false);

        ActivityUsed?.Invoke(totalPlayerBenefits, totalLabAffect);
    }

    private PlayerStats AccountMultipliers()
    {
        return new PlayerStats(
            (int)(requirements.Money * moneyMul), 
            (int)(requirements.Satiety * satietyMul), 
            (int)(requirements.Energy * energyMul));
    }

    public static void ChangeMultipliers(float energyMulChange,float moneyMulChange,float satietyMulChange)
    {
        energyMul += energyMulChange;
        satietyMul += satietyMulChange;
        moneyMul += satietyMulChange;
    }
    
    public void RecalculateActivityState()
    {
        if(isBlocked)
            return;
        
        if (wasUsedToday&&oneUsePerDay)
            return;

        if(weekdayOnly && !Week.currentDay.IsWeekDay())
        {
            OnActivityStateChanged?.Invoke(false);
            return;
        }

        if (timeRequired <= Week.HoursLeft && AccountMultipliers().CheckRequirements())
            OnActivityStateChanged?.Invoke(true);
        else OnActivityStateChanged?.Invoke(false);;
    }

    public void Disable()
    {
        numOfBlockingEffects++;
        OnActivityStateChanged?.Invoke(false);
    }

    public void Activate()
    {
        numOfBlockingEffects--;
        if(!isBlocked)
            OnActivityStateChanged?.Invoke(true);
    }

    public void Reset()
    {
        wasUsedToday = false;
    }

    public string GetRequirmentsString()
    {
        string requimentString = "";
        if (timeRequired > 0)
            requimentString += $"{timeRequired} часов ";
        requimentString += (AccountMultipliers()).GetString();
        return requimentString;
    }

}
