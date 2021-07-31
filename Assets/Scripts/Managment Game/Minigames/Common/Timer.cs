using UnityEngine;

/// <summary>
/// ??????
/// </summary>
public class Timer 
{
    private float timer;
    private int secondsLeft = 0;
    private int prevSecondsLeft = 0;
    
    public static event System.Action TimerRanOut;
    public static event System.Action<int> TimerValueChanged;

    public Timer(int seconds) => timer = seconds;
    
    /// <summary>
    /// ??? ??????? 
    /// </summary>
    public void Tick()
    {
        timer -= Time.deltaTime;
        secondsLeft = Mathf.FloorToInt(timer);
        if (prevSecondsLeft != secondsLeft)
        {
            TimerValueChanged?.Invoke(secondsLeft);
            prevSecondsLeft = secondsLeft;
        }
        if (timer <= 0)
            TimerRanOut?.Invoke();
    }
}
