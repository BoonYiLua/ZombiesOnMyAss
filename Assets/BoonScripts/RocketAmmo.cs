using UnityEngine;
using System.Collections;

public class RocketAmmo : MonoBehaviour {
    public int damageAmount = 50; // Amount of damage the rocket deals to zombies
    public float rocketSpeed = 10f; // Speed of the rocket
    public float lifetime = 3f; // Lifetime of the rocket before it despawns
    public GameObject explosionPrefab; // Prefab for the explosion effect

    private bool hitSomething = false;

    void Start() {
        // Start the coroutine to despawn the rocket
        StartCoroutine(DestroyAfterLifetime(lifetime));

        // Shoot the rocket forward when it's instantiated
        Rigidbody rocketRigidbody = GetComponent<Rigidbody>();
        rocketRigidbody.velocity = transform.forward * rocketSpeed;
    }

    void OnCollisionEnter(Collision other) {
        // Check if the rocket collides with any other collider.
        // You can use tags or layers to specify which objects you want the rocket to interact with.

        // In this example, we are checking if the other collider is tagged as "Zombie".
        if (other.gameObject.CompareTag("Zombie")) {
            // Get the ZombieController component of the zombie
            ZombieController zombieController = other.gameObject.GetComponent<ZombieController>();

            // If the zombie has a ZombieController component, deal damage to them
            if (zombieController != null) {
                zombieController.TakeDamage(damageAmount);
            }

            // Create an explosion effect at the collision point
            CreateExplosionEffect();

            // Destroy the rocket after hitting the zombie.
            Destroy(gameObject);
            hitSomething = true; // Set hitSomething to true since the rocket hit the zombie.
        } else if (other.gameObject.CompareTag("Wall")) {
            // Create an explosion effect at the collision point
            CreateExplosionEffect();

            // Destroy the rocket if it hits a wall.
            Destroy(gameObject);
            hitSomething = true; // Set hitSomething to true since the rocket hit the wall.
        }
    }

    private void CreateExplosionEffect() {
        // Instantiate the explosion prefab at the rocket's position and rotation
        Instantiate(explosionPrefab, transform.position, transform.rotation);
    }

    IEnumerator DestroyAfterLifetime(float delay) {
        // Wait for the specified lifetime.
        yield return new WaitForSeconds(delay);

        // Check if the rocket hasn't hit anything after the lifetime.
        if (!hitSomething) {
            // If it hasn't hit anything, create an explosion effect and destroy the rocket.
            CreateExplosionEffect();
            Destroy(gameObject);
        }
    }
}
