using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeThrower : MonoBehaviour {
    public GameObject grenadePrefab; // grenade prefab that will be thrown
    public int maxGrenadeCount = 3; // maximum number of grenades the player can throw
    public float throwForceMultiplier = 20f; // Multiplier to control the throw force (Increase this value to throw further)
    public float throwCurve = 1.0f; // Amount of curve to apply to the throw (adjust as needed)
    private int grenadeCount = 0; // current number of grenades thrown

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Q) && grenadeCount < maxGrenadeCount) {
            ThrowGrenade();
        }
    }

    void ThrowGrenade() {
        // Instantiate the grenade at the spawn point position
        GameObject grenade = Instantiate(grenadePrefab, transform.position, Quaternion.identity);

        // Get the Rigidbody component of the grenade (make sure it has one)
        Rigidbody rb = grenade.GetComponent<Rigidbody>();

        // Set a constant throw force value (e.g., 3.5f) multiplied by the throwForceMultiplier
        float throwForce = 3.5f * throwForceMultiplier;

        // Get the player's input direction
        Vector3 playerInputDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        playerInputDirection = playerInputDirection.normalized;

        // Apply the force in the calculated direction
        rb.AddForce(playerInputDirection * throwForce, ForceMode.Impulse);

        // Calculate a consistent rotation for the grenade based on the throwCurve
        Quaternion throwRotation = Quaternion.LookRotation(playerInputDirection + Vector3.up * throwCurve);
        rb.MoveRotation(throwRotation);

        grenadeCount++; // Increment the grenade count
    }
}
