using UnityEngine;
using TMPro;

/// <summary>
/// Попап результата проведенной активности
/// </summary>
public class ActivityUsedPopUp : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI textMesh;

    private const float disappearTime = 3;
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
        textMesh.text = $"Было восстановлено\n{playerStats.Energy} энергии, {playerStats.Satiety} сытости, {playerStats.Money} денег,\n" +
            $"{labStats.Complitness} законченности, {labStats.Correctness} правильности, {labStats.Originality} оригинальности";
        gameObject.SetActive(true);
        timeElapsed = 0;
        enabled = true;
    }
}
