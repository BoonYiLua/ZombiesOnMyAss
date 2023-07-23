using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Bullet : MonoBehaviour {
    private bool hitSomething = false;

    void Start() {
        // Start the coroutine to despawn the bullet after 2 seconds.
        StartCoroutine(DestroyAfterDelay(2f));
    }

    void OnTriggerEnter(Collider other) {
        // Check if the bullet collides with any other collider.
        // You can use tags or layers to specify which objects you want the bullet to interact with.

        // In this example, we are checking if the other collider is tagged as "Target".
        if (other.CompareTag("Target")) {
            // Set hitSomething to true since the bullet hit the target.
            hitSomething = true;

            // Destroy the target.
            Destroy(other.gameObject);

            // Destroy the bullet itself.
            Destroy(gameObject);
        }
    }

    IEnumerator DestroyAfterDelay(float delay) {
        // Wait for the specified delay.
        yield return new WaitForSeconds(delay);

        // Check if the bullet hasn't hit anything after the delay.
        if (!hitSomething) {
            // If it hasn't hit anything, destroy the bullet.
            Destroy(gameObject);
        }
    }
}
