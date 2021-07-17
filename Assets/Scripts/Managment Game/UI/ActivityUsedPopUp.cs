using UnityEngine;
using TMPro;

/// <summary>
/// ����� ���������� ����������� ����������
/// </summary>
public class ActivityUsedPopUp : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI textMesh;

    private const float disappearTime = 3;
    private float timeElapsed = 0;

    private void Start()
    {
        ActivityButton.ActivityUsed += ShowPopUp;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= disappearTime)
        {
            gameObject.SetActive(false);
            this.enabled = false;
        }
    }

    private void OnDestroy() => ActivityButton.ActivityUsed -= ShowPopUp;

    private void ShowPopUp(PlayerStats playerStats, LabStats labStats)
    {
        textMesh.text = $"���� �������������\n{playerStats.Energy} �������, {playerStats.Satiety} �������, {playerStats.Money} �����,\n" +
            $"{labStats.Complitness} �������������, {labStats.Correctness} ������������, {labStats.Originality} ��������������";
        gameObject.SetActive(true);
        timeElapsed = 0;
        this.enabled = true;
    }
}
