using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponPickup : MonoBehaviour {
    public int weaponIndex; // The index of the weapon in the PlayerController's weapons array

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Player")) {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            if (playerController != null) {
                playerController.PickupWeapon(weaponIndex);
                Destroy(gameObject); // Destroy the pickup object once it's picked up
            }
        }
    }
}
