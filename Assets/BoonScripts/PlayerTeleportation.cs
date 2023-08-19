using UnityEngine;

public class PlayerTeleportation : MonoBehaviour {
    public Transform targetTeleporter; // The other teleporter to teleport to.
    public float detectionRadius = 2.0f; // Radius within which the player can be detected.

    private void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            // Check if the player is within the detection radius of the teleporter.
            if (IsPlayerNearby()) {
                TeleportPlayer();
            }
        }
    }

    private bool IsPlayerNearby() {
        // Find all colliders within the detection radius of the teleporter.
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius);

        // Check if any of the colliders belong to an object with the "Player" tag.
        foreach (Collider collider in colliders) {
            if (collider.CompareTag("Player")) {
                return true; // Player is nearby.
            }
        }

        return false; // Player is not nearby.
    }

    private void TeleportPlayer() {
        // Find the player using the "Player" tag.
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null) {
            // Teleport the player to the target teleporter's position.
            player.transform.position = targetTeleporter.position;
        }
    }
}
