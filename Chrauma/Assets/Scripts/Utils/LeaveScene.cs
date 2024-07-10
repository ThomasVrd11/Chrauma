using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveLobby : MonoBehaviour
{
    [SerializeField] private int sceneIndex;
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.SwitchScene(sceneIndex);
        }
    }
}
