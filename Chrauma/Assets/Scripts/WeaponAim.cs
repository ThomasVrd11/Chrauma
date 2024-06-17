using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponAim : MonoBehaviour
{
    [SerializeField] public Transform player;
    private Transform weaponSlot;

    private void Awake()
    {
        weaponSlot = this.gameObject.transform;
    }
    void Update()
    {
        Vector3 mouseWorldPosition = GetMouseWorldPosition();
        Vector3 playerToMouseDir = mouseWorldPosition - player.position;

        // Convert the direction to a global rotation
        Quaternion lookRotation = Quaternion.LookRotation(playerToMouseDir);

        // Set the weapon slot's global rotation
        // Ensure we only modify the Y rotation if that's what's needed
        weaponSlot.rotation = Quaternion.Euler(0, lookRotation.eulerAngles.y, 0);
    }

    Vector3 GetMouseWorldPosition()
    {
        Plane plane = new Plane(Vector3.up, player.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out float enter))
        {
            return ray.GetPoint(enter);
        }
        return Vector3.zero;
    }
}