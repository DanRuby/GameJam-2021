using UnityEngine;
using TMPro;
using System.IO;
using System.Text;

/// <summary>
/// ОБертка для считывания массива стрингов 
/// Юнити не позволяет считывать массивы без него
/// </summary>
[System.Serializable]
class Wrapper<T>
{
    public T[] items;
}

[RequireComponent(typeof(RectTransform))]
/// <summary>
/// Контроллер попапов с общей информацией и активностей
/// </summary>
public class InfoPopUpController : MonoBehaviour
{
    #region Поля объекта
    [Header("Активности")]
    [Tooltip("Геймобджект паенли с информацией активностей")]
    [SerializeField]
    private GameObject activityPopUpGameObject;

    [Tooltip("Меш описания имени")]
    [SerializeField]
    private TextMeshProUGUI activityName;

    [Tooltip("Меш описания активности")]
    [SerializeField]
    private TextMeshProUGUI description;

    [Tooltip("Меш описания требований")]
    [SerializeField]
    private TextMeshProUGUI requirments;

    [Header("Информационная панель")]
    [Tooltip("Геймобджект паенли с информацией")]
    [SerializeField]
    private GameObject popUpGameObject;

    [Tooltip("Текст информационной панели")]
    [SerializeField]
    private TextMeshProUGUI infoPnaelText;
    #endregion


    private RectTransform infoPopUpRect;

    Wrapper<string> infoPanelsMessages;
    Wrapper<ActivityData> activitiesInfo;

    /// <summary>
    /// Класс для считывания информации из json активностей
    /// </summary>
    [System.Serializable]
    class ActivityData
    {
        public string name;
        public string description;
    }

    private void Awake()
    {
        
        string json = File.ReadAllText(Application.dataPath + "/StreamingAssets/InfoPanels.json", Encoding.UTF8);
        infoPanelsMessages = JsonUtility.FromJson<Wrapper<string>>(json);

        json = File.ReadAllText(Application.dataPath + "/StreamingAssets/Activities.json", Encoding.UTF8);
        activitiesInfo = JsonUtility.FromJson<Wrapper<ActivityData>>(json);

        activityPopUpGameObject.SetActive(false);
        popUpGameObject.SetActive(false);

        infoPopUpRect = popUpGameObject.GetComponent<RectTransform>();

        Subscribe();
    }

    private void Subscribe()
    {
        PopUpActivator.OnPopUpInfoShow += ShowInfoPopUp;
        PopUpActivator.OnPopUpInfoClose += HideInfoPopUp;

        ActivityButton.ActivityHoveredOver += ShowActivityInfoPopUp;
        ActivityButton.ActivityPointerExit += HideActivityInfoPopUp;
    }

    private void OnDestroy() => Unsubscribe();

    private void Unsubscribe()
    {
        ActivityButton.ActivityHoveredOver -= ShowActivityInfoPopUp;
        ActivityButton.ActivityPointerExit -= HideActivityInfoPopUp;

        PopUpActivator.OnPopUpInfoShow -= ShowInfoPopUp;
        PopUpActivator.OnPopUpInfoClose -= HideInfoPopUp;
    }

    #region Попап активностей
    private void ShowActivityInfoPopUp(Vector2 pos,int id,string requirmentsString)
    {
        activityName.text = activitiesInfo.items[id].name;
        description.text = activitiesInfo.items[id].description;
        requirments.text = requirmentsString;
        activityPopUpGameObject.transform.position = pos;
        activityPopUpGameObject.SetActive(true);
    }

    private void HideActivityInfoPopUp() 
    {
        activityPopUpGameObject.SetActive(false);
    }
    #endregion

    #region информационный попап
    private void ShowInfoPopUp(int id,Vector2 pos)
    {
        infoPnaelText.text = infoPanelsMessages.items[id];
        if (pos.x > 960)
            infoPopUpRect.pivot=new Vector2(1.0f, 1.0f);
        else infoPopUpRect.pivot = new Vector2(0, 1.0f);
        popUpGameObject.transform.position = pos;
        popUpGameObject.SetActive(true);
    }

    private void HideInfoPopUp()
    {
        popUpGameObject.SetActive(false);
    }
    #endregion
}
