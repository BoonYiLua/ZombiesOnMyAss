using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float movementSpeed = 5f;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private Rigidbody rb;
    private bool isGrounded;

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
    }
}
