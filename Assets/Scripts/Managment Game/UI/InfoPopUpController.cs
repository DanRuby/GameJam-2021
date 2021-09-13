using UnityEngine;
using TMPro;
using System.IO;
using System.Text;

/// <summary>
/// ������� ��� ���������� ������� �������� 
/// ����� �� ��������� ��������� ������� ��� ����
/// </summary>
[System.Serializable]
class Wrapper<T>
{
    public T[] items;
}

/// <summary>
/// Controller for activity and info popups
/// </summary>
[RequireComponent(typeof(RectTransform))]
public class InfoPopUpController : MonoBehaviour
{
    [Header("����������")]
    [Tooltip("����������� ������ � ����������� �����������")]
    [SerializeField]
    private GameObject activityPopUpGameObject;

    [Tooltip("��� �������� �����")]
    [SerializeField]
    private TextMeshProUGUI activityName;

    [Tooltip("��� �������� ����������")]
    [SerializeField]
    private TextMeshProUGUI description;

    [Tooltip("��� �������� ����������")]
    [SerializeField]
    private TextMeshProUGUI requirments;

    [Header("�������������� ������")]
    [Tooltip("����������� ������ � �����������")]
    [SerializeField]
    private GameObject popUpGameObject;

    [Tooltip("����� �������������� ������")]
    [SerializeField]
    private TextMeshProUGUI infoPnaelText;
    

    private RectTransform infoPopUpRect;
    Wrapper<string> infoPanelsMessages;
    

    private void Awake()
    {
        
        string json = File.ReadAllText(Application.dataPath + "/StreamingAssets/InfoPanels.json", Encoding.UTF8);
        infoPanelsMessages = JsonUtility.FromJson<Wrapper<string>>(json);

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

    #region Activity Popup
    private void ShowActivityInfoPopUp(Vector2 pos,Activity activity)
    {
        activityName.text = activity.Name;
        description.text = activity.description;
        requirments.text = activity.GetRequirmentsString();
        activityPopUpGameObject.transform.position = pos;
        activityPopUpGameObject.SetActive(true);
    }

    private void HideActivityInfoPopUp() 
    {
        activityPopUpGameObject.SetActive(false);
    }
    #endregion

    #region Info Popup
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
