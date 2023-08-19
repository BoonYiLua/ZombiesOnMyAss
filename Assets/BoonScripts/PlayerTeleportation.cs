using UnityEngine;

public class PlayerTeleportation : MonoBehaviour { 
    public Transform targetTeleporter; // The other teleporter to teleport to.

    private void Update() {
        if (Input.GetKeyDown(KeyCode.E)) // Check for "E" key press.
        {
            TeleportPlayer();
        }
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
