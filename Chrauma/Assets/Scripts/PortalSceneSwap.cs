using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SceneSwapPortal : MonoBehaviour
{
    public string sceneName;
    GameObject loadingScreen;
    
    private void Start() {
        loadingScreen = GameObject.Find("UI").transform.Find("Loading").gameObject;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadSceneAsync(sceneName);
            if (loadingScreen != null)
            {
            loadingScreen.SetActive(true);
            }
        }
    }
}
