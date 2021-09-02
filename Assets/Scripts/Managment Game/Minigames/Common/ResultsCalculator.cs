using UnityEngine;
using TMPro;

/// <summary>
/// Класс для вывода информации о проведенном измерении
/// </summary>
public class ResultsCalculator : MonoBehaviour
{
    private int maxComplitness = 15;
    private int maxCorrectness = 15;
    private int maxOriginality = 15;

    [SerializeField]
    private TextMeshProUGUI textMesh;

    private void Awake()
    {
        MiniGameManager.MiniGameEnded += PrintResult;
        gameObject.SetActive(false);
    }
    private void OnDestroy() => MiniGameManager.MiniGameEnded -= PrintResult;

    private void PrintResult(float result)
    {
        LabWork.Complitness = (int)(maxComplitness * result);
        LabWork.Correctness = (int)(maxCorrectness* result);
        LabWork.Originality = (int)(maxOriginality* result);
        textMesh.text = $"Результат вашей работы:\n Законченность - {LabWork.Complitness}, Правильность - {LabWork.Correctness}, Оригинальность - {LabWork.Originality}";
        gameObject.SetActive(true);
    }
}
