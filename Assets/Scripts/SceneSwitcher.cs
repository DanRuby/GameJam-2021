using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Script that switches between main scene and minigames and calls day event
/// </summary>
public class SceneSwitcher : MonoBehaviour
{
    [SerializeField]
    private string sceneName;
    
    [SerializeField]
    [Tooltip("Should this script call event about day end")]
    private bool isDayChange;

    public static event System.Action DayFinished;

    public void ChangeScene()
    {
        if (isDayChange)
            DayFinished?.Invoke();
        else SceneManager.LoadScene(sceneName);
    }
}
