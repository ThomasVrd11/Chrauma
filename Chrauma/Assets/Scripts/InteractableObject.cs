using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InteractableObject : MonoBehaviour
{
    public string interactMessage = "Press G to interact";
    public GameObject messagePrefab;
    private TMP_Text messageText;
    private bool isPlayerNearby = false;
    private bool messageDisplayed = false;
    [SerializeField] string messageAfter;

    void Start()
    {
        if (messagePrefab != null)
        {
            messageText = messagePrefab.transform.Find("MsgCanvas/Text (TMP)").GetComponent<TMP_Text>();
            messageText.text = "";
            messagePrefab.SetActive(false);
        }
    }

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.G))
        {
            StartCoroutine(DisplayMessage(messageAfter, 3f));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            if (!messageDisplayed)
            {
                StartCoroutine(DisplayMessage(interactMessage, 30f));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            if (messageDisplayed)
            {
                messagePrefab.SetActive(false);
                messageDisplayed = false;
            }
        }
    }
    // * Nico jv me tuer
    private IEnumerator DisplayMessage(string message, float delay)
    {
        if (messageText != null)
        {
        
            messagePrefab.SetActive(true);
            messageText.text = message;
            messageDisplayed = true;
            yield return new WaitForSeconds(delay);
            messagePrefab.SetActive(false);
            messageDisplayed = false;
        }
    }
}
