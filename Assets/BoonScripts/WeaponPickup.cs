using UnityEngine;

public class WeaponPickup : MonoBehaviour {
    public int weaponIndex; // The index of the weapon in the PlayerController's weapons array
    public float rotationSpeed = 60f; // The speed at which the pickup rotates

    // Reference to the PlayerController script
    public PlayerController playerController;

    private void Update() {
        // Rotate the pickup object around its up-axis (y-axis in most cases)
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Player")) {
            // Use the playerController reference to call the PickupWeapon method
            if (playerController != null) {
                playerController.PickupWeapon(weaponIndex);
                Destroy(gameObject); // Destroy the pickup object once it's picked up
            }
        }
    }
}
