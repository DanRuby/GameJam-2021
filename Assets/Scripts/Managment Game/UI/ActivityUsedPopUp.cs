using UnityEngine;
using TMPro;

/// <summary>
/// Popup for showing activity results
/// </summary>
public class ActivityUsedPopUp : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMesh;

    private const float disappearTime = 3; // seconds after which popup will disappear from screen
    private float timeElapsed = 0;

    private void Start()
    {
        Activity.ActivityUsed += ShowPopUp;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= disappearTime)
        {
            gameObject.SetActive(false);
            enabled = false;
        }
    }

    private void OnDestroy() => Activity.ActivityUsed -= ShowPopUp;

    private void ShowPopUp(PlayerStats playerStats, LabStats labStats)
    {
        textMesh.text = $"���� �������������\n{playerStats.Energy} �������, {playerStats.Satiety} �������, {playerStats.Money} �����,\n" +
            $"{labStats.Complitness} �������������, {labStats.Correctness} ������������, {labStats.Originality} ��������������";
        gameObject.SetActive(true);
        timeElapsed = 0;
        enabled = true;
    }
}
