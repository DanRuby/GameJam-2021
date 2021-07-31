using UnityEngine;
using System;
using UnityEngine.SceneManagement;

/// <summary>
/// пїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅ
/// </summary>
public enum Month
{
    January, February, March, April, May, June, 
    July, August, September, October, November, December, Unknown
}

public enum Day
{
    Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday, Unknown
}

[RequireComponent(typeof(RandomEventsManager))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(TeacherPicker))]
/// <summary>
/// пїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅ. пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅ пїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅ пїЅ пїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ.
/// </summary>
public class Week : MonoBehaviour
{
    #region пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ
    private const int HOURS_PER_DAY = 24;
    private  int MAX_ENERGY_REFILL = 35;
    private  int HOURS_TO_ENERGY_MULTIPLIER = 5;

    public const Day NEW_LAB_DAY = Day.Monday;
    public const Day LAB_DEFENCE_DAY = Day.Saturday;


    /// <summary>
    /// пїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅ пїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅ
    /// </summary>
    public static readonly int[] DaysInMonth = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
    #endregion

    /// <summary>
    /// пїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅ
    /// </summary>
    public static int CurrentDayDate
    {
        get => currentDayDate;
        private set
        {
            currentDay++;
            if (currentDay == Day.Unknown)
                currentDay = Day.Monday;


            if (DaysInMonth[(int)currentMonth] < value)
            {
                currentMonth++;
                if (currentMonth == Month.Unknown)
                    currentMonth = Month.January;
                currentDayDate = 1;
            }
            else currentDayDate = value;
        }
    }
    private static int currentDayDate = 1;

    /// <summary>
    /// пїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅ
    /// </summary>
    public static Day currentDay = Day.Sunday;

    /// <summary>
    /// пїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅ пїЅпїЅпїЅ
    /// </summary>
    public static int HoursLeft
    {
        get => hoursLeft;
        set
        {
            if (hoursLeft != value)
            {
                hoursLeft = value;
                HoursLeftChanged?.Invoke();
            }
        }
    }
    private static int hoursLeft = HOURS_PER_DAY;

    /// <summary>
    /// пїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅ
    /// </summary>
    public static Month currentMonth = Month.September;

    public static event Action DefenseDay;

    public static Teacher CurrentTeacher { get; private set; }

    public static event Action HoursLeftChanged;
    public static event Action DayChanged;

    private TeacherPicker teacherPicker;
    private RandomEventsManager eventsManager;
    private AudioSource audioSource;

    void Awake()
    {
        SceneSwitcher.DayFinished += GoToNextDay;

        eventsManager = GetComponent<RandomEventsManager>();
        teacherPicker = GetComponent<TeacherPicker>();
        audioSource = GetComponent<AudioSource>();
        DayChanged?.Invoke();
    }

    private void OnDestroy() => SceneSwitcher.DayFinished -= GoToNextDay;

    /// <summary>
    /// РњРµС‚РѕРґ РґР»СЏ РїРµСЂРµС…РѕРґР° РІ СЃР»РµРґСѓСЋС‰РёР№ РґРµРЅСЊ
    /// </summary>
    public void GoToNextDay()
    {
        CurrentDayDate++;
        DayChanged?.Invoke();

        CalculateSleepEnergy();

        HoursLeft = HOURS_PER_DAY;

        
        eventsManager.DeactivateCurrentEvent();
        if (currentDay == NEW_LAB_DAY)
        {
            CurrentTeacher = teacherPicker.PickNewTeacher();
            if (CurrentTeacher != null)
            {
                audioSource.clip = CurrentTeacher.audio;
                audioSource.Play(0);
                SceneManager.LoadScene(CurrentTeacher.SceneName);
            }
            else SceneManager.LoadScene("Congratulations");
        }
        else if (currentDay == LAB_DEFENCE_DAY)
            DefenseDay?.Invoke();
        else
            eventsManager.Tick();
    }

    private void CalculateSleepEnergy()
    {
        if (UpgradeActivator.ActiveUpgrades[(int) Upgrade.Matrace])
        {
            MAX_ENERGY_REFILL = 50;
            HOURS_TO_ENERGY_MULTIPLIER = 7;
        }

        Player.CurrentEnergy += Mathf.Min(MAX_ENERGY_REFILL, HoursLeft * HOURS_TO_ENERGY_MULTIPLIER);
    }
}