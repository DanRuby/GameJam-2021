using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// Controller of lab defenses
/// Stops the game if player has bad results and gives money with good results
/// </summary>
public class DefenseController: MonoBehaviour
{
    [SerializeField] private GameObject resultsObject;
    [SerializeField] private TextMeshProUGUI resultsText;
    [SerializeField] private Button resultsButton;
    
    private readonly int[] gradeMoney = { 2000, 3000 }; 
    private readonly int[] gradeDiffs = { 10, 20, 30 }; 
    private int grade;
   
    private void Awake()
    {
        Week.DefenseDay += FinishDefense;
        resultsObject.SetActive(false);
    }

    private void OnDestroy()
    {
        Week.DefenseDay -= FinishDefense;
    }
    
    
   private void FinishDefense()
    {
        CalculateGrade();

        resultsButton.onClick.RemoveAllListeners();

        if (grade <= 2)
        {
            resultsButton.onClick.AddListener(CloseApplication);
            resultsText.text = "По результатам защиты ва получи 2. Вы были отчислены";
        }
        else
        {
            if (grade >= 4)
            {
                Player.CurrentMoney += gradeMoney[grade - 4];
                resultsText.text = $"По результатам защиты вы получили {grade}. За хорошие успехи вы получили {gradeMoney[grade - 4]}";
            }
            else resultsText.text = "По результатам защиты вы получили 3";
            resultsButton.onClick.AddListener(SetActiveFalse);
        }
        resultsObject.SetActive(true);
    }

    public void CalculateGrade()
    {
        int maxDifference = Mathf.Max(Week.CurrentTeacher.Requirements.Complitness - LabWork.Complitness,
            Week.CurrentTeacher.Requirements.Correctness - LabWork.Correctness,
            Week.CurrentTeacher.Requirements.Originality - LabWork.Originality);

        if (maxDifference <= gradeDiffs[0])
            grade = 5;
        else if (maxDifference <= gradeDiffs[1])
            grade = 4;
        else if (maxDifference <= gradeDiffs[2])
            grade = 3;
        else grade = 2;

        if (Random.value <= Week.CurrentTeacher.Loyality)
            grade = Mathf.Clamp(grade + 1, 2, 5);

    }
    
    private void SetActiveFalse() => resultsObject.SetActive(false);

    private void CloseApplication() => Application.Quit();
}