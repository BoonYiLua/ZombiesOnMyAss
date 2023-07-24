using UnityEngine;

public class Medkit : MonoBehaviour {
    public int healAmount = 50; // Amount of health the medkit heals

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null) {
                playerController.PickupMedkit(healAmount, gameObject, gameObject); // Pass the medkit's heal amount and GameObject reference
            }
        }
    }
}
