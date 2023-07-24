using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour {
    public int damageAmount = 10; // Amount of damage the zombie deals to the player

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Player")) {
            // Get the PlayerController component of the player
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();

            // If the player has a PlayerController component, deal damage to them
            if (playerController != null) {
                playerController.TakeDamage(damageAmount);
            }
        }
    }
}
