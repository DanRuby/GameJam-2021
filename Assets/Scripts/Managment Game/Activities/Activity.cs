using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu()]
public class Activity : ScriptableObject
{
    [SerializeField] public string name;
    [SerializeField] public string description;
    [SerializeField] private bool oneUsePerDay;
    [SerializeField] private bool weekdayOnly;
    [SerializeField] private PlayerStats requirements;
    [SerializeField] private int timeRequired;
    
    [SerializeField] private PlayerStats baseBenefits;
    [SerializeField] private PlayerStats randomBenefits;
    [SerializeField] private LabStats baseLabBenefits;
    [SerializeField] private LabStats randomLabBenefits;

    private bool canBePerformed;
    private bool wasUsedToday;
    private int numOfBlockingEffects;

    private bool isBlocked => numOfBlockingEffects>0;

    private PlayerStats requiredStatsMultiplier; //TODO

    public event System.Action<bool> OnActivityStateChanged;
    public static event System.Action<PlayerStats, LabStats> ActivityUsed;
    
    public void PerformActivity()
    {
        wasUsedToday = true;

        float randomness = Random.value;
        PlayerStats randomAffectPlayer = randomBenefits * randomness;
        PlayerStats totalPlayerBenefits = baseBenefits + randomAffectPlayer;
        totalPlayerBenefits = Player.AccountSatietyDebuff(totalPlayerBenefits);
        Player.ChangeStats(totalPlayerBenefits - (requirements*requiredStatsMultiplier) );

        LabStats randomAffectLab = randomLabBenefits * randomness;
        LabStats totalLabAffect = baseLabBenefits + randomAffectLab;
        LabWork.ChangeValues(totalLabAffect);

        Week.HoursLeft -= timeRequired;
        if (oneUsePerDay)
            OnActivityStateChanged?.Invoke(false);

        ActivityUsed?.Invoke(totalPlayerBenefits, totalLabAffect);
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

        if (timeRequired <= Week.HoursLeft && (requirements*requiredStatsMultiplier).CheckRequirements())
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
        requimentString += (requirements*requiredStatsMultiplier).GetString();
        return requimentString;
    }

}
