using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// ����� ���������� ����������� � ������� �����
/// </summary>
public class UIController : MonoBehaviour
{


    [Tooltip("������ �� ������ �������")]
    [SerializeField]
    private GameObject teacherPanel;

 #region ��������� ����
    

    [Header("���� ������ ����")]
    [Tooltip("��� ��� ������ ��������������")]
    [SerializeField]
    private TextMeshProUGUI originalityTMP;

    [Tooltip("��� ��� ������ �������������")]
    [SerializeField]
    private TextMeshProUGUI complitnessTMP;

    [Tooltip("��� ��� ������ �������������")]
    [SerializeField]
    private TextMeshProUGUI correctnessTMP;

    [Header("���� ������ �������������")]
    [SerializeField]
    private TextMeshProUGUI FIOTMP;

    [SerializeField]
    private TextMeshProUGUI departmentTMP;

    [Tooltip("��� ��� ������ ��������������")]
    [SerializeField]
    private TextMeshProUGUI tOriginalityTMP;

    [Tooltip("��� ��� ������ �������������")]
    [SerializeField]
    private TextMeshProUGUI tComplitnessTMP;

    [Tooltip("��� ��� ������ �������������")]
    [SerializeField]
    private TextMeshProUGUI tCorrectnessTMP;

    [SerializeField]
    private Image tImage;
    #endregion

    #region ��������� �������

    private void ChangeOriginalityText(int newVal) => originalityTMP.text = $"{newVal}/{LabWork.MAX_STATS_VALUE}";

    private void ChangeComplitnessText(int newVal) => complitnessTMP.text = $"{newVal}/{LabWork.MAX_STATS_VALUE}";

    private void ChangeCorrectnessText(int newVal) => correctnessTMP.text = $"{newVal}/{LabWork.MAX_STATS_VALUE}";



    /// <summary>
    /// ������� �� ������� ����
    /// </summary>
    private void UnsubscribeFromEvents()
    {

        Week.DefenseDay -= HideLabUI;
        LabWork.ComplitnessValueChanged -= ChangeComplitnessText;
        LabWork.CorrectnessValueChanged -= ChangeCorrectnessText;
        LabWork.OriginalityValueChanged -= ChangeOriginalityText;


    }

    /// <summary>
    /// �������� �� ������� ����
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