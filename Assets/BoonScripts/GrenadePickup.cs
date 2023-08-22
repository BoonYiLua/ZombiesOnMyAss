using UnityEngine;

public class GrenadePickup : MonoBehaviour {
    // Reference to the GrenadeThrower script
    public GrenadeThrower grenadeThrower;
    public float rotationSpeed = 60f; // The speed at which the pickup rotates

    private void Update() {
        // Rotate the pickup object around its up-axis (y-axis in most cases)
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider collision) {
        if (collision.gameObject.CompareTag("Player")) {
            // Check if the player's grenade count is less than maxGrenadeCount before allowing pickup
            if (grenadeThrower != null && grenadeThrower.GrenadeCount < grenadeThrower.maxGrenadeCount) {
                grenadeThrower.PickupGrenade();
                Destroy(gameObject); // Destroy the pickup object once it's collected
            }
        }
    }
}
