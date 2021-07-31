using UnityEngine;

/// <summary>
/// Основная информация о преподавателе
/// </summary>
[CreateAssetMenu(fileName = "Teacher", menuName = "Scriptable Object/Teacher")]
public class Teacher : ScriptableObject
{
    /// <summary>
    /// ФИО
    /// </summary>
    public string FIO;

    /// <summary>
    /// Кафедра
    /// </summary>
    public Department Department;

    /// <summary>
    /// Требования
    /// </summary>
    public LabStats Requirements;

    /// <summary>
    /// Фотография
    /// </summary>
    public Sprite Photo;

    /// <summary>
    /// Название сцены с миниигрой
    /// </summary>
    public string SceneName;

    
    [Range(0,1)]
    [Tooltip(("Шанс преподавателя повысить оценку. Чем ближе к 1, тем больше шанс."))]
    public float Loyality;

    public AudioClip audio;
}
