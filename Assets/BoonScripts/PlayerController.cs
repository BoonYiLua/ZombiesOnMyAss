using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public float movementSpeed = 5f;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private Rigidbody rb;
    private bool isGrounded;

    public int currentWeapon = 0;
    public GameObject[] weapons; // Array of weapon GameObjects
    bool switchCooldown; 
    private void Awake() {
        rb = GetComponent<Rigidbody>();
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
            currentWeapon = Mathf.Clamp(currentWeapon, 0, weapons.Length - 1);
            SwitchWeapon(currentWeapon);
            StartCoroutine(waitSwitch());
        }
    }
    IEnumerator waitSwitch() {
        yield return new WaitForSeconds(0.5f);
        switchCooldown = false;
    }
    private void SwitchWeapon(int weaponIndex) {
        // Disable all weapons
        for (int i = 0; i < weapons.Length; i++) {
            weapons[i].SetActive(false);
        }

        // Enable the selected weapon
        weapons[weaponIndex].SetActive(true);
       
    }
}
