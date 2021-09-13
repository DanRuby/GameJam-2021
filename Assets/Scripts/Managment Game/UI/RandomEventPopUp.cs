using UnityEngine;
using System.IO;
using System.Text;
using TMPro;

/// <summary>
/// Random evnt popup
/// </summary>
public class RandomEventPopUp : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI randomEventName;
    [SerializeField] private TextMeshProUGUI randomEventDescription;

    private void Awake()
    {
        RandomEventsManager.RandomEventTriggered += ChangePopUpText;
        gameObject.SetActive(false);
    }

    private void OnDestroy() => RandomEventsManager.RandomEventTriggered -= ChangePopUpText;

    private void ChangePopUpText(string name, string description)
    {
        randomEventName.text = name;
        randomEventDescription.text = description;

        gameObject.SetActive(true);
    }

    public void TogglePopUpOff() => gameObject.SetActive(false);

}
