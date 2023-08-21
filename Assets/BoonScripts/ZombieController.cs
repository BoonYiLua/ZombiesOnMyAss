using UnityEngine;

public class ZombieController : MonoBehaviour {
    public int maxHealth = 100; // Maximum health of the zombie
    public int attackDamage = 10; // Amount of damage the zombie deals to the player
    private int currentHealth; // Current health of the zombie

    Animator Zombie;

    private void Start() {
        currentHealth = maxHealth; // Initialize the zombie's health to its maximum value
        Zombie = GetComponent<Animator>();
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

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Player")) {
            // Get the PlayerController component of the player
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();

            // If the player has a PlayerController component, deal damage to them
            if (playerController != null) {
                playerController.TakeDamage(attackDamage);
            }
        }
    }
}
