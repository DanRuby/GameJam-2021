using UnityEngine;

/// <summary>
/// �������� ���������� � �������������
/// </summary>
[CreateAssetMenu(fileName = "Teacher", menuName = "Scriptable Object/Teacher")]
public class Teacher : ScriptableObject
{
    /// <summary>
    /// ���
    /// </summary>
    public string FIO;

    /// <summary>
    /// �������
    /// </summary>
    public Department Department;

    /// <summary>
    /// ����������
    /// </summary>
    public LabStats Requirements;

    /// <summary>
    /// ����������
    /// </summary>
    public Sprite Photo;

    /// <summary>
    /// �������� ����� � ���������
    /// </summary>
    public string SceneName;

    [Range(0,1)]
    public float Loyality;

    public AudioClip audio;
}
