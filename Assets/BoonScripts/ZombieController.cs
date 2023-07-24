using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour {
    public int maxHealth = 100; // Maximum health of the zombie
    private int currentHealth;   // Current health of the zombie

    private void Start() {
        currentHealth = maxHealth; // Initialize the zombie's health to its maximum value
    }

    public void TakeDamage(int damageAmount) {
        currentHealth -= damageAmount;

        if (currentHealth <= 0) {
            Die();
        }
    }

    private void Die() {
        // Implement any death behavior for the zombie here.
        // For example, play a death animation or destroy the zombie object.
        Destroy(gameObject);
    }

    // Rest of the ZombieController script remains unchanged.
}
