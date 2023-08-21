using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour {
    public GameObject explosionParticlesPrefab; // Particle effect prefab for explosion

    public float explosionDelay = 3f; // Delay in seconds before the grenade explodes
    public float explosionRadius = 5f; // Radius of the explosion
    public float explosionForce = 500f; // Force of the explosion
    public int explosionDamage = 50; // Damage to apply to zombies within the explosion radius

    private bool hasExploded = false;

    void Start() {
        // Start the explosion countdown
        StartCoroutine(ExplodeAfterDelay());
    }

    IEnumerator ExplodeAfterDelay() {
        yield return new WaitForSeconds(explosionDelay);

        // Explode the grenade
        Explode();
    }

    void Explode() {
        if (hasExploded) return;

        // Spawn explosion particle effect
        GameObject particles = Instantiate(explosionParticlesPrefab, transform.position, Quaternion.identity);

        // Get all the colliders within the explosion radius
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider hitCollider in colliders) {
            // Apply explosion force to rigidbodies
            Rigidbody rb = hitCollider.GetComponent<Rigidbody>();
            if (rb != null) {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }

            // Check if the collider belongs to a zombie
            ZombieController zombieController = hitCollider.GetComponent<ZombieController>();
            if (zombieController != null) {
                // Calculate the damage based on the distance from the explosion point.
                float distance = Vector3.Distance(transform.position, hitCollider.transform.position);
                int damageToDeal = Mathf.RoundToInt(explosionDamage * (1 - distance / explosionRadius));

                // Deal damage to the zombie
                zombieController.TakeDamage(damageToDeal, transform.position);
            }
        }

        // Destroy the grenade and particle effect
        Destroy(particles, 2f);
        Destroy(gameObject);
        hasExploded = true;
    }
}
