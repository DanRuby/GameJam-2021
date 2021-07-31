using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    static DontDestroy instance = null;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance!=this)
            Destroy(gameObject);
            
        DontDestroyOnLoad(gameObject);
    }
}
