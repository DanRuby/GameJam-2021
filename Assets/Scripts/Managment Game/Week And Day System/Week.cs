using UnityEngine;
using System;
using UnityEngine.SceneManagement;

/// <summary>
/// ������ ����
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

/// <summary>
///In game week representations
/// </summary>
[RequireComponent(typeof(RandomEventsManager))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(TeachersPool))]
public class Week : MonoBehaviour
{
    private const int HOURS_PER_DAY = 24;
    public static int MAX_ENERGY_REFILL = 35;
    public static int HOURS_TO_ENERGY_MULTIPLIER = 5;

    public const Day NEW_LAB_DAY = Day.Monday;
    public const Day LAB_DEFENCE_DAY = Day.Saturday;
    public static readonly int[] DaysInMonth = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

    
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
    
    public static Day currentDay = Day.Sunday;
    
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
    
    public static Month currentMonth = Month.September;

    public static event Action DefenseDay;

    public static Teacher CurrentTeacher { get; private set; }

    public static event Action HoursLeftChanged;
    public static event Action DayChanged;

    private TeachersPool teachersPool;
    private RandomEventsManager eventsManager;
    private AudioSource audioSource;

    void Awake()
    {
        SceneSwitcher.DayFinished += ChangeToNextDay;

        eventsManager = GetComponent<RandomEventsManager>();
        teachersPool = GetComponent<TeachersPool>();
        audioSource = GetComponent<AudioSource>();
        DayChanged?.Invoke();
    }

    private void OnDestroy() => SceneSwitcher.DayFinished -= ChangeToNextDay;


    public void ChangeToNextDay()
    {
        CurrentDayDate++;
        DayChanged?.Invoke();

        CalculateSleepEnergy();

        HoursLeft = HOURS_PER_DAY;

        
        eventsManager.DeactivateCurrentEvent();
        if (currentDay == NEW_LAB_DAY)
        {
            CurrentTeacher = teachersPool.PickNewTeacher();
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
        Player.CurrentEnergy += Mathf.Min(MAX_ENERGY_REFILL, HoursLeft * HOURS_TO_ENERGY_MULTIPLIER);
    }
}