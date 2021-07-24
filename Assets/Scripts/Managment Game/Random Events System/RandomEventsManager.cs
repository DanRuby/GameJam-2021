using UnityEngine;

/// <summary>
/// �������� ��������� �������
/// </summary>
public class RandomEventsManager : MonoBehaviour
{
    /// <summary>
    /// ������ ��������� �������
    /// </summary>
    private readonly BaseRandomEvent[] events= {new MaterialAidRandomEvent(), new IllnessRandomEvent() , new HappyDayRandomEvent(), new FoodPoisoningRandomEvent(), 
                                                    new BreakUpRandomEvent(), new CockRoachesRandomEvent(), new FireTrainingRandomEvent() };

    /// <summary>
    /// ������� �������
    /// </summary>
    private BaseRandomEvent pickedEvent=null;

    public static event System.Action<int> RandomEventTriggered;

    public void Tick()
    {
        int pickedEventIndex = 0;
        float probablilty = Random.value;

        for (int i = 0; i < events.Length; i++)
        {
            BaseRandomEvent randomEvent = events[i];
            if (randomEvent.CanHappen && probablilty < randomEvent.Probability)
            {
                pickedEvent = randomEvent;
                pickedEventIndex = i;
            }
            randomEvent.Tick();
        }

        ActivatePickedEvent(pickedEventIndex);
    }

    private void ActivatePickedEvent(int pickedEventIndex)
    {
        if (pickedEvent != null)
        {
            pickedEvent.Activate();
            RandomEventTriggered?.Invoke(pickedEventIndex);
        }
    }

    public void DeactivateCurrentEvent()
    {
        if (pickedEvent != null)
        {
            pickedEvent.Deactivate();
            pickedEvent = null;
        }
    }

}
