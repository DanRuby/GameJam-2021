using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameCycleHandler : MonoBehaviour
{
    private Week week;
    private RandomEventsManager randomEventsManager;
    private TeachersPool teachersPool;
    
    public void GoToNextDay()
    {
        //Save();
        CalculateEnergyRestoration();
        week.ChangeToNextDay();
        randomEventsManager.DeactivateCurrentEvent();
        HandleSpecialDays();
    }

    private void CalculateEnergyRestoration()
    {
        
    }
    
    private void HandleSpecialDays()
    {
        /*if (week.currentDay == NEW_LAB_DAY)
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
            eventsManager.Tick();*/
    }
        
}
