using UnityEngine;
using UnityEngine.EventSystems;
using System;

/// <summary>
/// ACtivator for UI popups with additional info
/// </summary>
public class PopUpActivator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private int id; 

    public static event Action<int,Vector2> OnPopUpInfoShow;
    public static event Action OnPopUpInfoClose;

    public void OnPointerEnter(PointerEventData eventData) => OnPopUpInfoShow?.Invoke(id, eventData.position);

    public void OnPointerExit(PointerEventData eventData) => OnPopUpInfoClose?.Invoke();
}
