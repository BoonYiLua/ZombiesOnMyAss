using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {
    public float movementSpeed = 5f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public GameObject[] weapons; // Array of weapon GameObjects

    private Rigidbody rb;
    private bool isGrounded;
    private bool isNearAmmoBox = false;
    private AmmoBox currentAmmoBox = null;

    private int currentWeapon = 0;
    private List<GameObject> availableWeapons = new List<GameObject>();
    private bool switchCooldown;

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
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * movementSpeed;
        movement.y = rb.velocity.y; // Preserve the current vertical velocity
        rb.velocity = movement;

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

    // Method for picking up a new weapon
public void PickupWeapon(int weaponIndex) {
    if (weaponIndex >= 0 && weaponIndex < availableWeapons.Count) {
        // Activate the new weapon
        availableWeapons[weaponIndex].SetActive(true);

        // Update the current weapon index to the picked up weapon
        currentWeapon = weaponIndex;
    }
}

    // Method to get the current weapon index
    private int GetCurrentWeaponIndex() {
        for (int i = 0; i < availableWeapons.Count; i++) {
            if (availableWeapons[i].activeSelf) {
                return i;
            }
        }
        return 0; // Default to the first weapon if none is found active
    }
}
