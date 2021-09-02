using UnityEngine;

/// <summary>
/// пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅ
/// </summary>
public class RandomEventsManager : MonoBehaviour
{
    /// <summary>
    /// пїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅ
    /// </summary>
    [SerializeField]
    private RandomEvent[] events;

    /// <summary>
    /// пїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅ
    /// </summary>
    private RandomEvent pickedEvent=null;

    public static event System.Action<int> RandomEventTriggered;

    public void Tick()
    {
        int pickedEventIndex = 0;
        float probablilty = Random.value;

        for (int i = 0; i < events.Length; i++)
        {
            RandomEvent randomEvent = events[i];
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
