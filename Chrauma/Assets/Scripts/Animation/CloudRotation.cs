using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudRotation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /* rotate the gameobject*/
        this.transform.Rotate(new Vector3(0, 1, 0) * 4 * Time.deltaTime);
    }
}
