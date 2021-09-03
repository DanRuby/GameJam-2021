using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


[RequireComponent(typeof(Button))]
public class  ActivityButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Activity activity;

    public static event System.Action<Vector2, Activity> ActivityHoveredOver;
    public static event System.Action ActivityPointerExit;

    private Button button;

    /// <summary>
    /// пїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅ, пїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅ
    /// </summary>
    public void RecalculateActivityState()
    {
        activity.RecalculateActivityState();
    }

    void Awake()
    {
        button = GetComponent<Button>();

        activity.OnActivityStateChanged += (val) =>
        {
            button.interactable = val;
        };
        
        //TODO
        //UpgradeActivator.RecalculateRequiredStats(ref actualRequiredStats);
        
        AddConditions();
        RecalculateActivityState();
    }
    

    /// <summary>
    /// пїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅ
    /// </summary>
    private void AddConditions()
    {
        Week.DayChanged += HandleDayChanges;
        Week.HoursLeftChanged += RecalculateActivityState;
        Player.EnergyValueChanged += RecalculateActivityState;
        Player.MoneyValueChanged += RecalculateActivityState;
        Player.SatietyValueChanged += RecalculateActivityState;
    }

    private void OnDestroy()
    {
        Week.DayChanged -= HandleDayChanges;
        Week.HoursLeftChanged -= RecalculateActivityState;
        Player.EnergyValueChanged -= RecalculateActivityState;
        Player.MoneyValueChanged -= RecalculateActivityState;
        Player.SatietyValueChanged -= RecalculateActivityState;
    }

    /// <summary>
    /// пїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ
    /// </summary>
   public void PerformActivity() => activity.PerformActivity();

    public void HandleDayChanges() => activity.Reset();

    public void OnPointerEnter(PointerEventData eventData) => ActivityHoveredOver?.Invoke(eventData.position, activity);
    

    public void OnPointerExit(PointerEventData eventData) => ActivityPointerExit?.Invoke();
}
