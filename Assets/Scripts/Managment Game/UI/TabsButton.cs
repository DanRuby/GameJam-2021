using UnityEngine;

/// <summary>
/// Tab button for switching activity/upgrade tabs
/// </summary>
public class TabsButton : MonoBehaviour
{
    private int index;

    public static event System.Action<int> TabButtonPressed;

    private void Awake()
    {
        index = transform.GetSiblingIndex();    
    }

    public void ButtonPressed()
    {
        TabButtonPressed?.Invoke(index);
    }
}
