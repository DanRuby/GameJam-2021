using UnityEngine;
using UnityEngine.EventSystems;
using System;

/// <summary>
/// Активатор для показа справочной информации
/// </summary>
public class PopUpActivator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private int id; 

    public static Action<int,Vector2> OnPopUpInfoShow;
    public static Action OnPopUpInfoClose;

    public void OnPointerEnter(PointerEventData eventData) => OnPopUpInfoShow?.Invoke(id, eventData.position);

    public void OnPointerExit(PointerEventData eventData) => OnPopUpInfoClose?.Invoke();
}
