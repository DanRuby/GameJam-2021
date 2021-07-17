using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


[RequireComponent(typeof(Button))]
public class ActivityButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private int id;

    #region ѕол€ эдитора
    [Header("Requirements")]
    [Tooltip("јктивность можно выполнить только раз в день")]
    [SerializeField]
    private bool oncePerDay;

    [Tooltip("јктивность можно выполнить только в будни")]
    [SerializeField]
    private bool weekdayOnly;

    [Tooltip("Ќеобходимо времени дл€ выполнени€")]
    [SerializeField]
    private int timeRequired;

    [Tooltip("Ќеобходимые статы дл€ проведени€ активности")]
    [SerializeField]
    private PlayerStats requiredStats;

    [Space(4)]
    [Header("Benefits")]
    [Tooltip("¬осстанавливаемые параметры после проведени€ активности")]
    [SerializeField]
    private PlayerStats playerBenefits;

    [Tooltip("¬осстанавливаемые параметры после проведени€ активности")]
    [SerializeField]
    private LabStats labBenefits;

    [Space(4)]
    [Header("Random Benefits")]
    [Tooltip("¬осстанавливаемые параметры после проведени€ активности")]
    [SerializeField]
    private LabStats labBenefitsRandom;

    [Tooltip("¬осстанавливаемые параметры после проведени€ активности")]
    [SerializeField]
    private PlayerStats playerBenefitsRandom;
    #endregion

    public static System.Action<Vector2, int,string> ActivityHoveredOver;
    public static System.Action ActivityPointerExit;
    public static System.Action<PlayerStats, LabStats> ActivityUsed;

    /// ‘лаг, указывающий выполн€лось ли активность в текущем дне
    private bool activityWasUsed=false;

    private Button button;

    PlayerStats actualRequiredStats;

    /// <summary>
    /// ћетод перерасчета условий, чтобы пон€ть активной ли должна быть кнопка
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

        if (timeRequired <= Week.HoursLeft && actualRequiredStats.CheckRequirements())
            button.interactable = true;
        else button.interactable = false;
    }

    void Awake()
    {
        button = GetComponent<Button>();
        actualRequiredStats = requiredStats;

        UpgradeActivator.RecalculateRequiredStats(ref actualRequiredStats);

        Week.DayChanged += HandleDayChanges;

        IllnessRandomEvent.IllnesEventFired+=HandleIllnesActivated;
        IllnessRandomEvent.IllnesEventEnded+=HandleIllnesDeactivated;
        FoodPoisoningRandomEvent.FoodPoisoningEventFired+=HandleFoodPoisoningActivated;
        FoodPoisoningRandomEvent.FoodPoisoningEventEnded+=HandleFoodPoisoningDeactivated;

        AddConditions();
        HandleEvents();
    }

    private void HandleIllnesActivated(float multyplier) => actualRequiredStats.Energy += (int)(requiredStats.Energy * multyplier);
    private void HandleIllnesDeactivated(float multyplier) => actualRequiredStats.Energy -= (int)(requiredStats.Energy * multyplier);
    private void HandleFoodPoisoningActivated(float multyplier) => actualRequiredStats.Satiety -= (int)(requiredStats.Satiety * multyplier);
    private void HandleFoodPoisoningDeactivated(float multyplier) => actualRequiredStats.Satiety += (int)(requiredStats.Satiety * multyplier);

    /// <summary>
    /// ћетод добавлени€ условий дл€ проверки активности кнопки
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
        IllnessRandomEvent.IllnesEventFired -= HandleIllnesActivated;
        IllnessRandomEvent.IllnesEventEnded -= HandleIllnesDeactivated;
        FoodPoisoningRandomEvent.FoodPoisoningEventFired -= HandleFoodPoisoningActivated;
        FoodPoisoningRandomEvent.FoodPoisoningEventEnded -= HandleFoodPoisoningDeactivated;

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
    /// ћетод выполнени€ активности
    /// </summary>
   public void PerformActivity()
    {
        activityWasUsed = true;

        float randomness = Random.value;
        PlayerStats randomAffectPlayer = playerBenefitsRandom * randomness;
        PlayerStats totalPlayerBenefits = playerBenefits + randomAffectPlayer;
        totalPlayerBenefits = Player.AccountSatietyDebuff(totalPlayerBenefits);
        Player.ChangeStats(totalPlayerBenefits- actualRequiredStats);

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
            requimentString += $"{timeRequired} часов ";
        requimentString += actualRequiredStats.GetString();
        ActivityHoveredOver?.Invoke(eventData.position, id,requimentString);
    }

    public void OnPointerExit(PointerEventData eventData) => ActivityPointerExit?.Invoke();
}
