using UnityEngine;

public class GrenadePickup : MonoBehaviour {
    // Reference to the GrenadeThrower script
    public GrenadeThrower grenadeThrower;

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
