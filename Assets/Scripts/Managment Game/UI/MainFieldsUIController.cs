using UnityEngine;
using TMPro;

/// <summary>
/// UI controller of player and week fields and tab buttons controller
/// </summary>
public class MainFieldsUIController : MonoBehaviour
{

    [Tooltip("������ �� ����������� ������")]
    [SerializeField]
    private GameObject[] UIPanels;


    #region ��������� ����
    [Header("���� ������ ������")]
    [Tooltip("��� ��� ������ �����")]
    [SerializeField]
    private TextMeshProUGUI moneyTMP;

    [Tooltip("��� ��� ������ �������")]
    [SerializeField]
    private TextMeshProUGUI energyTMP;

    [Tooltip("��� ��� ������ �������")]
    [SerializeField]
    private TextMeshProUGUI satietyTMP;


    [Header("������� ������ ���")]
    [Tooltip("��� ��� ������ ����������� �������")]
    [SerializeField]
    private TextMeshProUGUI hoursTMP;

    [Tooltip("��� ��� ������ ���������� ���� ��� ������ ����")]
    [SerializeField]
    private TextMeshProUGUI newLabDaysTMP;

    [Tooltip("��� ��� ������ �������� ������")]
    [SerializeField]
    private TextMeshProUGUI currentMonthTMP;

    [Tooltip("��� ��� ������ ����������� ��� ��� ������ ����")]
    [SerializeField]
    private TextMeshProUGUI LabFinDaysTMP;

    [Tooltip("��� ��� ������ �������� ���")]
    [SerializeField]
    private TextMeshProUGUI DayTMP;

    #endregion

    #region ��������� �������
    private void ChangeEnergyText() => energyTMP.text = $"{Player.CurrentEnergy}/{Player.MaxEnergy}";

    private void ChangeMoneyText() => moneyTMP.text = $"{Player.CurrentMoney}";

    private void ChangeSatietyText()
    {
        satietyTMP.text = $"{Player.CurrentSatiety}/{Player.MaxSatiety}";
        if (Player.CurrentSatiety < Player.DebuffSatietyValue)
            satietyTMP.color = Color.red;
        else satietyTMP.color = Color.black;
    }

    private void ChangeTimeText() => hoursTMP.text = $"����� ��������: {Week.HoursLeft}";

    private void ChangeDayText()
    {
        currentMonthTMP.text = Week.currentMonth.ToRussianString();
        DayTMP.text = $"{Week.currentDay.ToRussianString()} - {Week.CurrentDayDate} �����";
        newLabDaysTMP.text = $"���� �� ������ ����: {Week.currentDay.DaysBefore(Week.NEW_LAB_DAY)}";
        LabFinDaysTMP.text = $"���� �� ������ ����: {Week.currentDay.DaysBefore(Week.LAB_DEFENCE_DAY)}";
    }

    /// <summary>
    /// ������� �� ������� ����
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
    /// �������� �� ������� ����
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
    /// ����� ��� ������������ ���������� ������������ �������
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
    /// ����� ���������� �������� �����������
    /// </summary>
    private void SetPanelsInactive()
    {
        foreach (GameObject obj in UIPanels)
            obj.SetActive(false);
    }
}
