using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField]
    private string sceneName;

    [SerializeField]
    private bool isDayChange;

    public static System.Action DayFinished;

    public void ChangeScene()
    {
        if (isDayChange)
            DayFinished?.Invoke();
        else SceneManager.LoadScene(sceneName);
    }
}
