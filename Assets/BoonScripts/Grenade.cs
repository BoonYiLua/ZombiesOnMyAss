using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour {
    public GameObject explosionParticlesPrefab; // Particle effect prefab for explosion

    public float explosionDelay = 3f; // Delay in seconds before the grenade explodes
    public float explosionRadius = 5f; // Radius of the explosion
    public float explosionForce = 500f; // Force of the explosion

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
        }

        // Destroy the grenade and particle effect
        Destroy(particles, 2f);
        Destroy(gameObject);
        hasExploded = true;
    }
}