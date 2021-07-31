using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField]
    private string sceneName;
    
    [SerializeField]
    [Tooltip("Р”РѕР»Р¶РЅРѕ Р»Рё РїРѕ РѕРєРѕРЅС‡Р°РЅРёСЋ Р°РЅРёРјР°С†РёРё РІС‹Р·РІР°С‚СЊСЃСЏ СЃРѕР±С‹С‚РёРµ РѕР± РѕРєРѕРЅС‡Р°РЅРёРё РґРЅСЏ ")]
    private bool isDayChange;

    public static event System.Action DayFinished;

    public void ChangeScene()
    {
        if (isDayChange)
            DayFinished?.Invoke();
        else SceneManager.LoadScene(sceneName);
    }
}
