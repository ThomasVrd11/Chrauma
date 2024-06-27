using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowTextOnProxy : MonoBehaviour
{
    public GameObject player;
    public TMP_Text proximityText;
    public string message = "Displayedtext";
    private bool isTextDisplayed = false;

    void Start()
    {
        proximityText.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player && !isTextDisplayed)
        {
            StartCoroutine(DisplayText());
        }
    }

    void Update()
    {
    }

    private IEnumerator DisplayText()
    {
    isTextDisplayed = true;
    proximityText.text = message;
    proximityText.enabled = true;

    yield return new WaitForSeconds(3f);
    proximityText.enabled = false;
    isTextDisplayed = false;
    transform.gameObject.SetActive(false); 
    }
}
