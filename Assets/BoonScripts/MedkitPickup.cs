using UnityEngine;

public class MedkitPickup : MonoBehaviour {
    public int healAmount = 50; // Amount of health restored by the medkit
    public float rotationSpeed = 60f; // The speed at which the pickup rotates

    // Reference to the PlayerController script
    public PlayerController playerController;
    public GameObject medkitPrefab;

    private void Update() {
        // Rotate the pickup object around its up-axis (y-axis in most cases)
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider collision) {
        if (collision.gameObject.CompareTag("Player")) {
            // Use the playerController reference to call the PickupMedkit method
            if (playerController != null) {
                playerController.PickupMedkit(healAmount, medkitPrefab, gameObject);
            }
        }
    }
}
