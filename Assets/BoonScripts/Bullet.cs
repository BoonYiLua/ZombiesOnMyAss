using UnityEngine;
using System.Collections;


public class Bullet : MonoBehaviour {
    private bool hitSomething = false;

    void Start() {
        // Start the coroutine to despawn the bullet 
        StartCoroutine(DestroyAfterDelay(0.8f));
    }

    void OnCollisionEnter(Collision other) {
        // Check if the bullet collides with any other collider.
        // You can use tags or layers to specify which objects you want the bullet to interact with.

        // In this example, we are checking if the other collider is tagged as "Target".
        if (other.gameObject.CompareTag("Target")) {
            // Destroy the target.
            Destroy(other.gameObject);
            hitSomething = true; // Set hitSomething to true since the bullet hit the target.
        } else if (other.gameObject.CompareTag("Wall")) {
            // Destroy the bullet if it hits a wall.
            Destroy(gameObject);
            hitSomething = true; // Set hitSomething to true since the bullet hit the wall.
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
