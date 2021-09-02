using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


[RequireComponent(typeof(Button))]
public class ActivityButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private int id;

    #region пїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅ
    [Header("Requirements")]
    [Tooltip("пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅ пїЅ пїЅпїЅпїЅпїЅ")]
    [SerializeField]
    private bool oncePerDay;

    [Tooltip("пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅ пїЅ пїЅпїЅпїЅпїЅпїЅ")]
    [SerializeField]
    private bool weekdayOnly;

    [Tooltip("пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ")]
    [SerializeField]
    private int timeRequired;

    [Tooltip("пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ")]
    [SerializeField]
    private PlayerStats requiredStats;

    [Space(4)]
    [Header("Benefits")]
    [Tooltip("пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ")]
    [SerializeField]
    private PlayerStats playerBenefits;

    [Tooltip("пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ")]
    [SerializeField]
    private LabStats labBenefits;

    [Space(4)]
    [Header("Random Benefits")]
    [Tooltip("пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ")]
    [SerializeField]
    private LabStats labBenefitsRandom;

    [Tooltip("пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ")]
    [SerializeField]
    private PlayerStats playerBenefitsRandom;
    #endregion

    public static event System.Action<Vector2, int,string> ActivityHoveredOver;
    public static event System.Action ActivityPointerExit;
    public static event System.Action<PlayerStats, LabStats> ActivityUsed;

    /// пїЅпїЅпїЅпїЅ, пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅ
    private bool activityWasUsed=false;

    private Button button;

    private static PlayerStats requiredStatsMultiplier=new PlayerStats(1,1,1);

    /// <summary>
    /// пїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅ, пїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅ
    /// </summary>
    public void HandleEvents()
    {
        if (activityWasUsed&&oncePerDay)
            return;

        if(weekdayOnly && !Week.currentDay.IsWeekDay())
        {
            button.interactable = false;
            return;
        }

        if (timeRequired <= Week.HoursLeft && (requiredStats*requiredStatsMultiplier).CheckRequirements())
            button.interactable = true;
        else button.interactable = false;
    }

    void Awake()
    {
        button = GetComponent<Button>();
       
        //TODO
        //UpgradeActivator.RecalculateRequiredStats(ref actualRequiredStats);

        Week.DayChanged += HandleDayChanges;

        AddConditions();
        HandleEvents();
    }

    public static void ChangeRequiredStatsMultiplier(PlayerStats multiplier)
    {
        requiredStatsMultiplier += multiplier;
    }

    /// <summary>
    /// пїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅ
    /// </summary>
    private void AddConditions()
    {
        if (timeRequired > 0)
            Week.HoursLeftChanged += HandleEvents;
        if (requiredStats.Energy > 0)
            Player.EnergyValueChanged += HandleEvents;
        if (requiredStats.Money > 0)
            Player.MoneyValueChanged += HandleEvents;
        if (requiredStats.Satiety > 0)
            Player.SatietyValueChanged += HandleEvents;
    }

    private void OnDestroy()
    {
        Week.DayChanged -= HandleDayChanges;

        if (timeRequired > 0)
            Week.HoursLeftChanged -= HandleEvents;
        if (requiredStats.Energy > 0)
            Player.EnergyValueChanged -= HandleEvents;
        if (requiredStats.Money > 0)
            Player.MoneyValueChanged -= HandleEvents;
        if (requiredStats.Satiety > 0)
            Player.SatietyValueChanged -= HandleEvents;
    }

    /// <summary>
    /// пїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ
    /// </summary>
   public void PerformActivity()
    {
        activityWasUsed = true;

        float randomness = Random.value;
        PlayerStats randomAffectPlayer = playerBenefitsRandom * randomness;
        PlayerStats totalPlayerBenefits = playerBenefits + randomAffectPlayer;
        totalPlayerBenefits = Player.AccountSatietyDebuff(totalPlayerBenefits);
        Player.ChangeStats(totalPlayerBenefits - (requiredStats*requiredStatsMultiplier) );

        LabStats randomAffectLab = labBenefitsRandom * randomness;
        LabStats totalLabAffect = labBenefits + randomAffectLab;
        LabWork.ChangeValues(totalLabAffect);

        Week.HoursLeft -= timeRequired;
        if (oncePerDay)
            button.interactable = false;

        ActivityUsed?.Invoke(totalPlayerBenefits, totalLabAffect);
    }

    public void HandleDayChanges() => activityWasUsed = false;

    public void OnPointerEnter(PointerEventData eventData) 
    {
        string requimentString = string.Empty;
        if (timeRequired > 0)
            requimentString += $"{timeRequired} пїЅпїЅпїЅпїЅпїЅ ";
        requimentString += (requiredStats*requiredStatsMultiplier).GetString();
        ActivityHoveredOver?.Invoke(eventData.position, id,requimentString);
    }

    public void OnPointerExit(PointerEventData eventData) => ActivityPointerExit?.Invoke();
}
