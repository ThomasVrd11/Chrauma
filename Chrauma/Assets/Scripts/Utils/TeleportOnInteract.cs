using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class TeleportOnInteract : MonoBehaviour
{
    // * Variables for the teleportation
    [SerializeField] Transform receivingPortal;
    private bool isTeleporting = false;
    private bool playerIsInTrigger = false;
    private GameObject player;
    private GameObject playerTrail;
    private CharacterController characterController;
    private bool debugMode = false;


    void Start()
    {
        /** assign objects to variables **/
        player = GameObject.FindWithTag("Player");
        playerTrail = player.gameObject.transform.Find("ghost/TrailReap").gameObject;
        characterController = player.GetComponent<CharacterController>();


    }
    // * I setup input to G because Geleportation

    void Update()
    {
        /** if player is in the trigger and is pressing G key, start teleporting **/
        if (playerIsInTrigger && Input.GetKeyDown(KeyCode.G) && !isTeleporting)
        {
            StartCoroutine(TeleportPlayer());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        /* check if it's the player that entered the trigger */
        if (other.CompareTag("Player"))
        {
            if (debugMode) Debug.Log("Player entered trigger");
            playerIsInTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        /* turn bool to false if player leave the trigger */
        if (other.CompareTag("Player"))
        {
            if (debugMode) Debug.Log("Player exited trigger");
            playerIsInTrigger = false;
        }
    }
    private IEnumerator TeleportPlayer()
    {
        /* Teleportation logic */
        playerTrail.gameObject.SetActive(false);
        isTeleporting = true;
        Quaternion portalRotationDifference = receivingPortal.rotation * Quaternion.Inverse(transform.rotation);
        portalRotationDifference *= Quaternion.Euler(0f, 180f, 0f);

        Vector3 positionOffset = player.transform.position - transform.position;
        positionOffset = portalRotationDifference * positionOffset;
        Vector3 newPosition = receivingPortal.position + positionOffset;

        characterController.enabled = false;
        player.transform.SetPositionAndRotation(newPosition, player.transform.rotation * portalRotationDifference);
        playerIsInTrigger = false;
        characterController.enabled = true;
        playerTrail.gameObject.SetActive(true);
        isTeleporting = false;
        yield return null;
    }
}
