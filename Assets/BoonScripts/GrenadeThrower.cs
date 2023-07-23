using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeThrower : MonoBehaviour {
    public GameObject grenadePrefab; // grenade prefab that will be thrown
    public int maxGrenadeCount = 3; // maximum number of grenades the player can throw
    public float throwForceMultiplier = 10f; // Multiplier to control the throw force
    private int grenadeCount = 0; // current number of grenades thrown

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Q) && grenadeCount < maxGrenadeCount) {
            ThrowGrenade();
        }
    }

    void ThrowGrenade() {
        // Instantiate the grenade at the spawn point position and rotation
        GameObject grenade = Instantiate(grenadePrefab, transform.position, Quaternion.identity);

        // Get the Rigidbody component of the grenade (make sure it has one)
        Rigidbody rb = grenade.GetComponent<Rigidbody>();

        // Set a constant throw force value (e.g., 3.5f)
        float throwForce = 3.5f;

        // Get the player's input direction
        Vector3 playerInputDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        playerInputDirection = playerInputDirection.normalized;

        // Convert the input direction to the world space (relative to the player's rotation)
        Vector3 throwDirection = transform.TransformDirection(playerInputDirection);

        // Apply the force in the calculated direction
        rb.AddForce(throwDirection * throwForce, ForceMode.Impulse);

        grenadeCount++; // Increment the grenade count
    }
}
