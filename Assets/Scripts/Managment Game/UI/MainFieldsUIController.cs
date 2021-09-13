using UnityEngine;
using TMPro;

/// <summary>
/// UI controller of player and week fields and tab buttons controller
/// </summary>
public class MainFieldsUIController : MonoBehaviour
{

    [Tooltip("Ссылки на всплывающие панели")]
    [SerializeField]
    private GameObject[] UIPanels;


    #region Текстовые поля
    [Header("Поля статов игрока")]
    [Tooltip("Меш для вывода денег")]
    [SerializeField]
    private TextMeshProUGUI moneyTMP;

    [Tooltip("Меш для вывода энергии")]
    [SerializeField]
    private TextMeshProUGUI energyTMP;

    [Tooltip("Меш для вывода сытости")]
    [SerializeField]
    private TextMeshProUGUI satietyTMP;


    [Header("Полядля вывода дня")]
    [Tooltip("Меш для вывода оставшегося времени")]
    [SerializeField]
    private TextMeshProUGUI hoursTMP;

    [Tooltip("Меш для вывода оставшихся дней для снятия лабы")]
    [SerializeField]
    private TextMeshProUGUI newLabDaysTMP;

    [Tooltip("Меш для вывода текущего месяца")]
    [SerializeField]
    private TextMeshProUGUI currentMonthTMP;

    [Tooltip("Меш для вывода оставшеихся дня для защиты лабы")]
    [SerializeField]
    private TextMeshProUGUI LabFinDaysTMP;

    [Tooltip("Меш для вывода текущего дня")]
    [SerializeField]
    private TextMeshProUGUI DayTMP;

    #endregion

    #region Обработка событий
    private void ChangeEnergyText() => energyTMP.text = $"{Player.CurrentEnergy}/{Player.MaxEnergy}";

    private void ChangeMoneyText() => moneyTMP.text = $"{Player.CurrentMoney}";

    private void ChangeSatietyText()
    {
        satietyTMP.text = $"{Player.CurrentSatiety}/{Player.MaxSatiety}";
        if (Player.CurrentSatiety < Player.DebuffSatietyValue)
            satietyTMP.color = Color.red;
        else satietyTMP.color = Color.black;
    }

    private void ChangeTimeText() => hoursTMP.text = $"Часов осталось: {Week.HoursLeft}";

    private void ChangeDayText()
    {
        currentMonthTMP.text = Week.currentMonth.ToRussianString();
        DayTMP.text = $"{Week.currentDay.ToRussianString()} - {Week.CurrentDayDate} число";
        newLabDaysTMP.text = $"Дней до снятия лабы: {Week.currentDay.DaysBefore(Week.NEW_LAB_DAY)}";
        LabFinDaysTMP.text = $"Дней до защиты лабы: {Week.currentDay.DaysBefore(Week.LAB_DEFENCE_DAY)}";
    }

    /// <summary>
    /// Отписка от событий игры
    /// </summary>
    private void UnsubscribeFromEvents()
    {
        Player.MoneyValueChanged -= ChangeMoneyText;
        Player.EnergyValueChanged -= ChangeEnergyText;
        Player.SatietyValueChanged -= ChangeSatietyText;

        Week.HoursLeftChanged -= ChangeTimeText;
        Week.DayChanged -= ChangeDayText;

        TabsButton.TabButtonPressed -= ToggleUIPanelActive;
    }

    /// <summary>
    /// Подписка на события игры
    /// </summary>
    private void SubscribeToEvents()
    {
        Player.MoneyValueChanged += ChangeMoneyText;
        Player.EnergyValueChanged += ChangeEnergyText;
        Player.SatietyValueChanged += ChangeSatietyText;

        Week.HoursLeftChanged += ChangeTimeText;
        Week.DayChanged += ChangeDayText;

        TabsButton.TabButtonPressed += ToggleUIPanelActive;
    }
    #endregion

    private void Awake()
    {
        SetPanelsInactive();
        SubscribeToEvents();
        SetTexts();
    }

    private void SetTexts()
    {
        ChangeDayText();
        ChangeTimeText();

        ChangeEnergyText();
        ChangeMoneyText();
        ChangeSatietyText();
    }

    private void OnDestroy() => UnsubscribeFromEvents();


    /// <summary>
    /// Метод для переключения активности необходимого канваса
    /// </summary>
    /// <param name="index"></param>
    private void ToggleUIPanelActive(int index)
    {
        for (int i = 0; i < UIPanels.Length; i++)
            if (i != index)
                UIPanels[i].SetActive(false);
        UIPanels[index].SetActive(!UIPanels[index].activeSelf);
    }

    /// <summary>
    /// Метод отключения канвасов активностей
    /// </summary>
    private void SetPanelsInactive()
    {
        foreach (GameObject obj in UIPanels)
            obj.SetActive(false);
    }
}
