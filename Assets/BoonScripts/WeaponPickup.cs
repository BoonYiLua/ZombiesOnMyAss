using UnityEngine;

public class WeaponPickup : MonoBehaviour {
    public int weaponIndex; // The index of the weapon in the PlayerController's weapons array
    public float rotationSpeed = 60f; // The speed at which the pickup rotates

    // Reference to the PlayerController script
    public PlayerController playerController;
    public GameObject GunPrefab;

    private void Update() {
        // Rotate the pickup object around its up-axis (y-axis in most cases)
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider collision) {
        if (collision.gameObject.CompareTag("Player")) {
            // Use the playerController reference to call the PickupWeapon method
            if (playerController != null) {
                playerController.Pickup(playerController.CheckPickup(), GunPrefab, gameObject);

   
            }
        }
    }
}
