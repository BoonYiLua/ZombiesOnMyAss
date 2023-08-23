using UnityEngine;
using UnityEngine.UI;

public class FollowPlayerText : MonoBehaviour {
    public Transform player;  // Reference to the player's transform
    public Text textToFollow;  // Reference to the Text UI element you want to follow the player

    private void Update() {
        if (player != null && textToFollow != null) {
            // Convert the player's world position to screen space
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(player.position);

            // Set the position of the text element to follow the player
            textToFollow.transform.position = screenPosition;
        }
    }
}
