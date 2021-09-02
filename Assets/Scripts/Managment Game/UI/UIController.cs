using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// Класс управления интерфейсом в главной сцене
/// </summary>
public class UIController : MonoBehaviour
{


    [Tooltip("Ссылки на панель препода")]
    [SerializeField]
    private GameObject teacherPanel;

 #region Текстовые поля
    

    [Header("Поля статов лабы")]
    [Tooltip("Меш для вывода оригинальности")]
    [SerializeField]
    private TextMeshProUGUI originalityTMP;

    [Tooltip("Меш для вывода завершенности")]
    [SerializeField]
    private TextMeshProUGUI complitnessTMP;

    [Tooltip("Меш для вывода праавильности")]
    [SerializeField]
    private TextMeshProUGUI correctnessTMP;

    [Header("Поля вывода преподавателя")]
    [SerializeField]
    private TextMeshProUGUI FIOTMP;

    [SerializeField]
    private TextMeshProUGUI departmentTMP;

    [Tooltip("Меш для вывода оригинальности")]
    [SerializeField]
    private TextMeshProUGUI tOriginalityTMP;

    [Tooltip("Меш для вывода завершенности")]
    [SerializeField]
    private TextMeshProUGUI tComplitnessTMP;

    [Tooltip("Меш для вывода праавильности")]
    [SerializeField]
    private TextMeshProUGUI tCorrectnessTMP;

    [SerializeField]
    private Image tImage;
    #endregion

    #region Обработка событий

    private void ChangeOriginalityText(int newVal) => originalityTMP.text = $"{newVal}/{LabWork.MAX_STATS_VALUE}";

    private void ChangeComplitnessText(int newVal) => complitnessTMP.text = $"{newVal}/{LabWork.MAX_STATS_VALUE}";

    private void ChangeCorrectnessText(int newVal) => correctnessTMP.text = $"{newVal}/{LabWork.MAX_STATS_VALUE}";



    /// <summary>
    /// Отписка от событий игры
    /// </summary>
    private void UnsubscribeFromEvents()
    {

        Week.DefenseDay -= HideLabUI;
        LabWork.ComplitnessValueChanged -= ChangeComplitnessText;
        LabWork.CorrectnessValueChanged -= ChangeCorrectnessText;
        LabWork.OriginalityValueChanged -= ChangeOriginalityText;


    }

    /// <summary>
    /// Подписка на события игры
    /// </summary>
    private void SubscribeToEvents()
    {

        Week.DefenseDay += HideLabUI;
        LabWork.ComplitnessValueChanged += ChangeComplitnessText;
        LabWork.CorrectnessValueChanged += ChangeCorrectnessText;
        LabWork.OriginalityValueChanged += ChangeOriginalityText;

    }
    #endregion

    private void Awake()
    {
        ShowTeacherInfo();
        SubscribeToEvents();
        SetTexts();
    }

    private void SetTexts()
    {
        ChangeComplitnessText(LabWork.Complitness);
        ChangeCorrectnessText(LabWork.Correctness);
        ChangeOriginalityText(LabWork.Originality);
    }

    private void OnDestroy() => UnsubscribeFromEvents();

    private void ShowTeacherInfo()
    {
        if (Week.CurrentTeacher == null)
            teacherPanel.SetActive(false);
        else
        {
            tImage.sprite = Week.CurrentTeacher.Photo;
            FIOTMP.text = Week.CurrentTeacher.FIO;
            departmentTMP.text = Week.CurrentTeacher.Department.ToRussianString();
            tComplitnessTMP.text = Week.CurrentTeacher.Requirements.Complitness.ToString();
            tOriginalityTMP.text = Week.CurrentTeacher.Requirements.Originality.ToString();
            tCorrectnessTMP.text = Week.CurrentTeacher.Requirements.Correctness.ToString();
            teacherPanel.SetActive(true);
        }
    }

    private void HideLabUI() => teacherPanel.SetActive(false);


}