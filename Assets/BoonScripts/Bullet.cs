using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public float bulletSpeed = 20f;
    public float bulletLifetime = 2f;

    private void Awake() {
        Destroy(gameObject, bulletLifetime);
    }

    private void Update() {
        // Move the bullet forward based on its speed
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other) {
        // Check if the bullet has hit something with a collider (excluding triggers and the player)
        if (!other.isTrigger && !other.CompareTag("Player")) {
            // Destroy the bullet
            Destroy(gameObject);
        }
    }
}
