using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetBlueOrb : MonoBehaviour
{
    [SerializeField] GameObject blueBridge;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player")
        {
        blueBridge.SetActive(true);
        gameObject.SetActive(false);
        }
    }
}
