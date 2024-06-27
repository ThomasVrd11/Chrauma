using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentManager : MonoBehaviour
{
    public static PersistentManager instance;

    private void Awake() {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
    }

}
