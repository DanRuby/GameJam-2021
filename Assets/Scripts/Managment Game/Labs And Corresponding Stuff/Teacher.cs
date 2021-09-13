using UnityEngine;

/// <summary>
/// Class that stores teacher parameters and data
/// </summary>
[CreateAssetMenu(fileName = "Teacher", menuName = "Scriptable Object/Teacher")]
public class Teacher : ScriptableObject
{
    /// <summary>
    /// Name string
    /// </summary>
    public string FIO;
    
    public Department Department;

    /// <summary>
    /// Lab requirements to get at least a 3 on defense 
    /// </summary>
    public LabStats Requirements;
    
    public Sprite Photo;

    /// <summary>
    /// Minigame scene
    /// </summary>
    public string SceneName;

    
    [Range(0,1)]
    [Tooltip(("Chance of increasing player`s mark. Bigger the number, bigger the chance"))]
    public float Loyality;

    public AudioClip audio;
}
