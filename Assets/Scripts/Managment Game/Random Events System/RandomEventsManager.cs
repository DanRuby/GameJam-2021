using UnityEngine;

public class RandomEventsManager : MonoBehaviour
{
    [SerializeField] private RandomEvent[] events;
    
    private RandomEvent pickedEvent=null;// Current random event that is active

    public static event System.Action<string,string> RandomEventTriggered;

    public void Tick()
    {
        int pickedEventIndex = 0;
        float probability = Random.value;

        //pick first available event that is not on cooldown and has higher probability that the one we got
        for (int i = 0; i < events.Length; i++)
        {
            RandomEvent randomEvent = events[i];
            if (randomEvent.CanOccur && probability < randomEvent.Probability)
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
            RandomEventTriggered?.Invoke(pickedEvent.Name,pickedEvent.Description);
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
