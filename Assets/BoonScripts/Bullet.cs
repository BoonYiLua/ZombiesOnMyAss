using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public float bulletLifetime = 2f; // Time in seconds before the bullet is destroyed

    private void Start() {
        // Set the bullet to be destroyed after a certain amount of time
        Destroy(gameObject, bulletLifetime);
    }

    private void OnTriggerEnter(Collider other) {
        // Check if the bullet has hit something with a collider (excluding triggers)
        if (!other.isTrigger) {
            // Destroy the bullet when it hits an object
            Destroy(gameObject);
        }
    }
}
