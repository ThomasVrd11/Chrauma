using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PortalSameScene : MonoBehaviour
{
    public Transform receivingPortal;
    public bool isTeleporting = false;
    private float tpCooldown = 1.5f;
    private PortalSameScene portalsamescene;

    void Start()
    {
        portalsamescene = receivingPortal.GetComponent<PortalSameScene>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isTeleporting)
        {
            StartCoroutine(TeleportPlayer(other));
        }
    }

    private IEnumerator TeleportPlayer(Collider playerCollider)
    {
        portalsamescene.isTeleporting = true;
        CharacterController player = playerCollider.GetComponent<CharacterController>();
        Quaternion portalRotationDifference = receivingPortal.rotation * Quaternion.Inverse(transform.rotation);
        portalRotationDifference *= Quaternion.Euler(0f, 180f, 0f);

        Vector3 positionOffset = playerCollider.transform.position - transform.position;
        positionOffset = portalRotationDifference * positionOffset;
        Vector3 newPosition = receivingPortal.position + positionOffset;
        player.enabled = false;
        playerCollider.transform.SetPositionAndRotation(newPosition, playerCollider.transform.rotation * portalRotationDifference);
        player.enabled = true;

        yield return new WaitForSeconds(tpCooldown);
        portalsamescene.isTeleporting = false;
    }
}
