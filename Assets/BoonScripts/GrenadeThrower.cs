using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrenadeThrower : MonoBehaviour {
    public GameObject grenadePrefab; // Grenade prefab that will be thrown
    public int maxGrenadeCount = 3; // Maximum number of grenades the player can hold
    public float throwForceMultiplier = 20f; // Multiplier to control the throw force (Increase this value to throw further)
    public float throwCurve = 1.0f; // Amount of curve to apply to the throw (adjust as needed)
    public int grenadeCount = 0; // Current number of grenades held
    public GameObject throwTarget; // The target GameObject to determine the throw direction

    public GameObject activeGrenadeObject; // GameObject for the active grenade image
    public GameObject inactiveGrenadeObject; // GameObject for the inactive (grayed out) grenade image

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Q)) {
            ThrowGrenade();
        }
    }

    public void PickupGrenade() {
        grenadeCount += 1;

        // Activate the active grenade GameObject
        if (activeGrenadeObject != null) {
            activeGrenadeObject.SetActive(true);
        }

        // Deactivate the inactive grenade GameObject
        if (inactiveGrenadeObject != null) {
            inactiveGrenadeObject.SetActive(false);
        }
    }

    public int GrenadeCount {
        get { return grenadeCount; }
    }

    public void ThrowGrenade() {
        if (grenadeCount <= 0) return;
        // Ensure there is a target before throwing
        if (throwTarget == null) {
            Debug.LogWarning("Throw target not set.");
            return;
        }

        // Calculate the throw direction based on the target position
        Vector3 throwDirection = (throwTarget.transform.position - transform.position).normalized;

        // Instantiate the grenade at the spawn point position
        GameObject grenade = Instantiate(grenadePrefab, transform.position, Quaternion.identity);

        // Get the Rigidbody component of the grenade (make sure it has one)
        Rigidbody rb = grenade.GetComponent<Rigidbody>();

        // Set a constant throw force value (e.g., 3.5f) multiplied by the throwForceMultiplier
        float throwForce = 3.5f * throwForceMultiplier;

        // Apply the force in the calculated direction
        rb.AddForce(throwDirection * throwForce, ForceMode.Impulse);

        // Calculate a consistent rotation for the grenade based on the throwCurve
        Quaternion throwRotation = Quaternion.LookRotation(throwDirection + Vector3.up * throwCurve);
        rb.MoveRotation(throwRotation);

        // Decrease the grenade count after throwing
        grenadeCount--;

        // If there are no grenades left, deactivate the active grenade GameObject
        // and activate the inactive (grayed out) grenade GameObject
        if (grenadeCount <= 0) {
            if (activeGrenadeObject != null) {
                activeGrenadeObject.SetActive(false);
            }

            if (inactiveGrenadeObject != null) {
                inactiveGrenadeObject.SetActive(true);
            }
        }
    }
}
