using UnityEngine;

public class PersistentManager : MonoBehaviour
{
    /* Singleton reference */
    public static PersistentManager instance;

    private void Awake() {
        /* Singleton pattern */
        if (instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
    }

}
