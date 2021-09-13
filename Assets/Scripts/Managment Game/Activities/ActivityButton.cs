using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


/// <summary>
/// Button by pressing which player performs activity
/// </summary>
[RequireComponent(typeof(Button))]
public class  ActivityButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Activity activity;

    public static event System.Action<Vector2, Activity> ActivityHoveredOver;
    public static event System.Action ActivityPointerExit;

    private Button button;
    
    void Awake()
    {
        button = GetComponent<Button>();
        AddConditions();

        activity.OnActivityStateChanged += (val) =>
        {
            button.interactable = val;
        };
        activity.RecalculateActivityState();
    }

    /// <summary>
    /// Subscription to in-game events that will update activity state 
    /// </summary>
    private void AddConditions()
    {
        Week.DayChanged += activity.Reset;
        Week.HoursLeftChanged += activity.RecalculateActivityState;
        Player.EnergyValueChanged += activity.RecalculateActivityState;
        Player.MoneyValueChanged += activity.RecalculateActivityState;
        Player.SatietyValueChanged += activity.RecalculateActivityState;
    }

    private void OnDestroy()
    {
        Week.DayChanged -= activity.Reset;
        Week.HoursLeftChanged -= activity.RecalculateActivityState;
        Player.EnergyValueChanged -= activity.RecalculateActivityState;
        Player.MoneyValueChanged -= activity.RecalculateActivityState;
        Player.SatietyValueChanged -= activity.RecalculateActivityState;
    }
    
   public void PerformActivity() => activity.PerformActivity();

   public void OnPointerEnter(PointerEventData eventData) => ActivityHoveredOver?.Invoke(eventData.position, activity);

   public void OnPointerExit(PointerEventData eventData) => ActivityPointerExit?.Invoke();
}
