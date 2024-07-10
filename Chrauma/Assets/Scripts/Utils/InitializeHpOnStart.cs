using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeHpOnStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerStats.instance)
        {
            PlayerStats.instance.InitializeUI();
        }
    }

}
