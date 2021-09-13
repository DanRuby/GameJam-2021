using UnityEngine;

/// <summary>
/// Don`t destroy on load component
/// </summary>
public class DontDestroy : MonoBehaviour
{
    static DontDestroy instance = null;

    private void Awake()
    {
        if (instance==null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        
        Destroy(gameObject);
    }
}
