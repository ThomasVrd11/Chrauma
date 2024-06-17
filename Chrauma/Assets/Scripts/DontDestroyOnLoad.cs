using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    private void Awake()
    {
        /* don't destroy this gameobject when changing scene */
        DontDestroyOnLoad(this);
    }
}
