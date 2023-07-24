using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {
    public float movementSpeed = 5f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public GameObject[] weapons; // Array of weapon GameObjects
    public int health = 100; // Player's health

    private Rigidbody rb;
    private bool isGrounded;
    private bool isNearAmmoBox = false;
    private AmmoBox currentAmmoBox = null;

    private int currentWeapon = 0;
    private List<GameObject> availableWeapons = new List<GameObject>();
    private bool switchCooldown;

    private bool isDowned = false; // Indicates whether the player is in a downed state or not

    private void Awake() {
        rb = GetComponent<Rigidbody>();
        InitializeWeapons();
    }

    private void InitializeWeapons() {
        for (int i = 0; i < weapons.Length; i++) {
            weapons[i].SetActive(false);
            availableWeapons.Add(weapons[i]);
        }
        SwitchWeapon(currentWeapon);
    }

    private void Update() {
        // Check if the player is grounded
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.1f, groundLayer);

        // Player movement
        if (!isDowned) {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * movementSpeed;
            movement.y = rb.velocity.y; // Preserve the current vertical velocity
            rb.velocity = movement;
        }

        // Weapon switch
        if (!isDowned && Input.GetAxisRaw("Mouse ScrollWheel") != 0) {
            if (switchCooldown) return;
            switchCooldown = true;
            currentWeapon += (int)Mathf.Sign(Input.GetAxisRaw("Mouse ScrollWheel"));
            currentWeapon = Mathf.Clamp(currentWeapon, 0, availableWeapons.Count - 1);
            SwitchWeapon(currentWeapon);
            StartCoroutine(waitSwitch());
        }

        // Interaction with ammo box
        if (!isDowned && isNearAmmoBox && Input.GetKeyDown(KeyCode.E)) {
            if (currentAmmoBox != null) {
                currentAmmoBox.ClaimAmmo();
            }
        }

        // Handle player health
        if (!isDowned && health <= 0) {
            Die(); // Player is dead if health drops to or below zero
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("AmmoBox")) {
            isNearAmmoBox = true;
            currentAmmoBox = other.GetComponent<AmmoBox>();
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("AmmoBox")) {
            isNearAmmoBox = false;
            currentAmmoBox = null;
        }
    }

    private void SwitchWeapon(int weaponIndex) {
        // Disable all weapons
        for (int i = 0; i < availableWeapons.Count; i++) {
            availableWeapons[i].SetActive(false);
        }

        // Enable the selected weapon
        availableWeapons[weaponIndex].SetActive(true);
    }

    private IEnumerator waitSwitch() {
        yield return new WaitForSeconds(0.5f);
        switchCooldown = false;
    }

    // Method for dealing damage to the player
    public void TakeDamage(int damageAmount) {
        if (!isDowned && health > 0) {
            health -= damageAmount;
            if (health <= 0) {
                health = 0; // Ensure health doesn't go below zero
                Die(); // Player is dead if health drops to zero
            }
        }
    }

    // Method for player's death
    private void Die() {
        // Add any actions you want to happen when the player dies, like game over, respawn, etc.
        isDowned = true; // Set the player to a downed state
    }

    // Method to revive the player
    public void Revive() {
        isDowned = false; // Set the player back to normal state
        health = 100; // Set the health back to maximum or any other value you prefer for revival
    }

    // Method for picking up a new weapon
    public void PickupWeapon(int weaponIndex) {
        if (weaponIndex >= 0 && weaponIndex < availableWeapons.Count) {
            // Activate the new weapon
            availableWeapons[weaponIndex].SetActive(true);

            // Update the current weapon index to the picked up weapon
            currentWeapon = weaponIndex;
        }
    }
}
