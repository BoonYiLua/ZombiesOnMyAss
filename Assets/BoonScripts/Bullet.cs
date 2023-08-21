using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
    public int damageAmount = 25; // Amount of damage the bullet deals to zombies
    public GameObject explosionPrefab; // Prefab for the explosion effect
    private bool hitSomething = false;

    void Start() {
        // Start the coroutine to despawn the bullet
        StartCoroutine(DestroyAfterDelay(0.8f));
    }

    void OnCollisionEnter(Collision other) {
        if (!hitSomething) { // Only process the collision if the bullet hasn't hit anything yet
            if (other.gameObject.CompareTag("Zombie")) {
                // Get the ZombieController component of the zombie
                ZombieController zombieController = other.gameObject.GetComponent<ZombieController>();

                // If the zombie has a ZombieController component, deal damage to them
                if (zombieController != null) {
                    zombieController.TakeDamage(damageAmount, transform.position); // Pass hit point
                }

                // Create an explosion effect at the collision point
                CreateExplosionEffect();

                // Destroy the bullet after hitting the zombie.
                Destroy(gameObject);
                hitSomething = true; // Set hitSomething to true since the bullet hit the zombie.
            } else if (other.gameObject.CompareTag("Wall")) {
                // Create an explosion effect at the collision point
                CreateExplosionEffect();

                // Destroy the bullet if it hits a wall.
                Destroy(gameObject);
                hitSomething = true; // Set hitSomething to true since the bullet hit the wall.
            }
        }
    }

    private void CreateExplosionEffect() {
        // Instantiate the explosion prefab at the bullet's position and rotation
        Instantiate(explosionPrefab, transform.position, transform.rotation);
    }

    IEnumerator DestroyAfterDelay(float delay) {
        // Wait for the specified delay.
        yield return new WaitForSeconds(delay);

        // Check if the bullet hasn't hit anything after the delay.
        if (!hitSomething) {
            // If it hasn't hit anything, create an explosion effect and destroy the bullet.
            CreateExplosionEffect();
            Destroy(gameObject);
        }
    }
}
