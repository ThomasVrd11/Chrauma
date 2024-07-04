using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentManager : MonoBehaviour
{
    public static PersistentManager instance;
    /** Script to keep a gameobject from scene to scene (mainly used for singleton managers) **/
    private void Awake() {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
    }

}
