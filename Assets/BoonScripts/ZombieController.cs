using UnityEngine;

public class ZombieController : MonoBehaviour {
    public int maxHealth = 100; // Maximum health of the zombie
    public int attackDamage = 10; // Amount of damage the zombie deals to the player
    private int currentHealth; // Current health of the zombie

    Animator Zombie;
    public ParticleSystem damageParticlesPrefab; // Reference to the Particle System prefab
    private ParticleSystem damageParticles; // Reference to the instantiated Particle System

    private void Start() {
        currentHealth = maxHealth; // Initialize the zombie's health to its maximum value
        Zombie = GetComponent<Animator>();
    }

    public void TakeDamage(int damageAmount, Vector3 hitPoint) {
        if (damageAmount > 0) {
            currentHealth -= damageAmount;

            if (currentHealth <= 0) {
                Die();
            } else {
                // Check if a Particle System prefab is assigned and if damageParticles is null
                if (damageParticlesPrefab != null) {
                    // Instantiate the Particle System from the prefab at the hit point
                    damageParticles = Instantiate(damageParticlesPrefab, hitPoint, Quaternion.identity);

                    // Calculate the rotation to face the hit point
                    Vector3 directionToHitPoint = hitPoint - transform.position;
                    Quaternion rotationToHitPoint = Quaternion.LookRotation(directionToHitPoint);

                    // Apply the calculated rotation to the particle system
                    damageParticles.transform.rotation = rotationToHitPoint;

                    // Play the instantiated particle effect
                    PlayDamageParticles();
                }
            }
        }
    }

    private void PlayDamageParticles() {
        if (damageParticles != null) {
            // Play the instantiated particle effect
            damageParticles.Play();
        }
    }

    private void Die() {
        // Play the particle effect at the Zombie's position
        if (damageParticlesPrefab != null) {
            damageParticles = Instantiate(damageParticlesPrefab, transform.position, Quaternion.identity);
            PlayDamageParticles();
        }

        // Destroy the Zombie
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            // Get the PlayerController component of the player
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();

            // If the player has a PlayerController component, deal damage to them
            if (playerController != null) {
                playerController.TakeDamage(attackDamage);
                Debug.Log("damage");
            }
        }
    }
}
