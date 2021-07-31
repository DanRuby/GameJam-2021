using UnityEngine;
using System.IO;
using System.Text;
using TMPro;

/// <summary>
/// Попап случайного события
/// </summary>
public class RandomEventPopUp : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI randomEventName;

    [SerializeField]
    private TextMeshProUGUI randomEventDescription;

    #region Json

    [System.Serializable]
    class EventDescriptionArgs
    {
        public string name;
        public string description;
    }

    Wrapper<EventDescriptionArgs> eventDescriptions;
    

    #endregion

    private void Awake()
    {
        string json = File.ReadAllText(Application.dataPath + "/StreamingAssets/RandomEvents.json", Encoding.UTF8);
        eventDescriptions = JsonUtility.FromJson<Wrapper<EventDescriptionArgs>>(json);

        RandomEventsManager.RandomEventTriggered += ChangePopUpText;

        gameObject.SetActive(false);
    }

    private void OnDestroy() => RandomEventsManager.RandomEventTriggered -= ChangePopUpText;

    private void ChangePopUpText(int index)
    {
        randomEventName.text = eventDescriptions.items[index].name;
        randomEventDescription.text = eventDescriptions.items[index].description;

        gameObject.SetActive(true);
    }

    public void TogglePopUpOff() => gameObject.SetActive(false);

}
