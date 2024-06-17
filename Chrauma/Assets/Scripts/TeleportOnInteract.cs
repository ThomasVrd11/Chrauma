using UnityEngine;
using System.Collections;

public class TeleportOnInteract : MonoBehaviour
{
    public Transform receivingPortal;
    public bool isTeleporting = false;
    private float tpCooldown = 1.5f;
    private PortalSameScene portalsamescene;
    private bool playerIsInTrigger = false;


    void Start()
    {
        portalsamescene = receivingPortal.GetComponent<PortalSameScene>();
    }

    void Update()
    {
        if (playerIsInTrigger && Input.GetKeyDown(KeyCode.G) && !isTeleporting)
        {
            StartCoroutine(TeleportPlayer());
        }
    }

    private void TriggerEnterOn(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered trigger");
            playerIsInTrigger = true;
        }
    }

    private void TriggerExitOn(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited trigger");
            playerIsInTrigger = false;
        }
    }

    private IEnumerator TeleportPlayer()
    {
        isTeleporting = true;
        portalsamescene.isTeleporting = true;
        GameObject player = GameObject.FindWithTag("Player");
        CharacterController characterController = player.GetComponent<CharacterController>();

        Quaternion portalRotationDifference = receivingPortal.rotation * Quaternion.Inverse(transform.rotation);
        portalRotationDifference *= Quaternion.Euler(0f, 180f, 0f);

        Vector3 positionOffset = player.transform.position - transform.position;
        positionOffset = portalRotationDifference * positionOffset;
        Vector3 newPosition = receivingPortal.position + positionOffset;

        characterController.enabled = false;
        player.transform.SetPositionAndRotation(newPosition, player.transform.rotation * portalRotationDifference);
        characterController.enabled = true;

        yield return new WaitForSeconds(tpCooldown);

        isTeleporting = false;
        portalsamescene.isTeleporting = false;
    }
}
