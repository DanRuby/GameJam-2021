using UnityEngine;

public class TabsButton : MonoBehaviour
{
    private int index;

    public static System.Action<int> TabButtonPressed;

    private void Awake()
    {
        index = transform.GetSiblingIndex();    
    }

    public void ButtonPressed()
    {
        TabButtonPressed?.Invoke(index);
    }
}
