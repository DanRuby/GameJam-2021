using UnityEngine;
using TMPro;

/// <summary>
/// Class that controls timer interface
/// </summary>
public class TimerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMesh;

    private void Awake() => Timer.TimerValueChanged += ChangeText;

    private void OnDestroy() => Timer.TimerValueChanged -= ChangeText;

    private void ChangeText(int newSeconds) => textMesh.text = newSeconds.ToString();
}
