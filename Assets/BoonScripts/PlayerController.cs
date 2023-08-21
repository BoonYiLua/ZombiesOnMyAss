using UnityEngine;
using System.Collections; 

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
    public System.Collections.Generic.List<GameObject> availableWeapons = new System.Collections.Generic.List<GameObject>();
    private bool switchCooldown;
    public int weaponTotal;

    public int grenadeCount = 0;

    public bool isDowned = false; // Indicates whether the player is in a downed state or not
    public float reviveDelay = 20f; // Time in seconds before the player revives

    Animator PlayerMovement;

    [Header("Player Health")]
    public int maxHealth = 100;
    public int currentHealth; // Serialized variable for current health
    private bool isEquippedMedkit; // Indicates whether the player has the medkit equipped
    private GameObject equippedMedkit; // Reference to the currently equipped medkit
    private int medkitCount = 1; // Number of available medkits

    private void Awake() {
        currentHealth = health; // Initialize the current health to the maximum health on Awake
                                //InitializeWeapons();

    }

    private void Start() {
        rb = GetComponent<Rigidbody>();
        currentHealth = health;
        PlayerMovement = GetComponent<Animator>();
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
            Debug.Log(horizontalInput); 
            Debug.Log(verticalInput);
            PlayerMovement.SetFloat("MoveX", horizontalInput);
            PlayerMovement.SetFloat("MoveY", verticalInput);
            Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);
            movement.y = rb.velocity.y; // Preserve the current vertical velocity
            Debug.Log(movementSpeed);
            rb.velocity = movement*movementSpeed;

            // Weapon switch
            if (Input.GetAxisRaw("Mouse ScrollWheel") != 0) {
                if (switchCooldown) return;
                switchCooldown = true;
                currentWeapon += (int)Mathf.Sign(Input.GetAxisRaw("Mouse ScrollWheel"));
                currentWeapon = Mathf.Clamp(currentWeapon, 0, availableWeapons.Count - 1);
                SwitchWeapon(currentWeapon);
                StartCoroutine(waitSwitch());
            }

            // Interaction with ammo box
            if (isNearAmmoBox && Input.GetKeyDown(KeyCode.E)) {
                if (currentAmmoBox != null) {
                    currentAmmoBox.ClaimAmmo();
                }
            }

            // Handle player health
            if (currentHealth <= 0) {
                Die(); // Player is dead if health drops to or below zero
            }
        }


        // Right-click to use the equipped medkit
        if (isEquippedMedkit && Input.GetMouseButtonDown(1)) {
            UseMedkit();
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

    private System.Collections.IEnumerator waitSwitch() {
        yield return new WaitForSeconds(0.5f);
        switchCooldown = false;
    }

    public void TakeDamage(int damageAmount) {
        if (!isDowned && currentHealth > 0) {
            currentHealth -= damageAmount;
            if (currentHealth <= 0) {
                currentHealth = 0; // Ensure health doesn't go below zero
                Die(); // Player is dead if health drops to zero
            }
        }
    }


    private IEnumerator DownedCoroutine() {
        isDowned = true; // Set the player to a downed state
        yield return new WaitForSeconds(reviveDelay);
        Revive(); // Revive the player after the delay
    }

    private void Die() {
        // Add any actions you want to happen when the player dies, like game over, respawn, etc.
        if (!isDowned) {
            StartCoroutine(DownedCoroutine()); // Start the downed coroutine if not already downed
        }
    }

    public void Revive() {
        isDowned = false; // Set the player back to the normal state
        currentHealth = Mathf.Min(maxHealth, currentHealth + 40); 
    }

    public int CheckPickup() {
        weaponTotal = 0;
        foreach (GameObject W in availableWeapons) {
            if (weaponTotal < 2) {
                W.GetComponent<GunController>();
                weaponTotal++;
            }
        }
        return weaponTotal;
    }

    public void Pickup(int weaponTotal, GameObject weapon, GameObject weaponPickup) {
        if (weaponTotal < 2) {
            availableWeapons.Add(weapon);
            Destroy(weaponPickup);
        }
    }
    public void AddGrenade() {
        grenadeCount++;
    }

    public void SwapWeapon(int weaponIndex, GameObject weapon, GameObject weaponPickup) {
        weapons[weaponIndex] = null;
        weapons[weaponIndex] = weapon;
        Destroy(weaponPickup);
        //Update visuals
    }

    public void PickupWeapon(int weaponIndex) {
        if (weaponIndex >= 0 && weaponIndex < availableWeapons.Count) {
            // Activate the new weapon
            availableWeapons[weaponIndex].SetActive(true);

            // Update the current weapon index to the picked up weapon
            currentWeapon = weaponIndex;
        }
    }

    public void PickupMedkit(int healAmount, GameObject medkitObject, GameObject pickupObject) {
        if (!isEquippedMedkit && medkitCount > 0) {
            isEquippedMedkit = true;
            equippedMedkit = medkitObject;
            medkitCount--;

            // Show some visual indicator that the player has the medkit equipped (e.g., highlight the medkit object)
            // For example, you can change the medkit's material or add some glowing effect.

            // Add the heal amount to the current health but make sure it doesn't exceed the maximum health.
            currentHealth = Mathf.Min(currentHealth + healAmount, maxHealth);

            // Destroy the medkit since it's been used.
            Destroy(pickupObject);
        }
    }

    public void UseMedkit() {
        // Ensure the player can't use the medkit while in a downed state or at full health.
        if (!isDowned && currentHealth < maxHealth) {
            int healAmount = equippedMedkit.GetComponent<Medkit>().healAmount;

            // Calculate how much the player can be healed without exceeding the maximum health.
            int potentialHealth = currentHealth + healAmount;
            if (potentialHealth > maxHealth) {
                healAmount = maxHealth - currentHealth;
            }

            // Heal the player by the calculated heal amount.
            currentHealth += healAmount;

            // Remove the equipped medkit.
            isEquippedMedkit = false;
            Destroy(equippedMedkit);
        }
    }
}