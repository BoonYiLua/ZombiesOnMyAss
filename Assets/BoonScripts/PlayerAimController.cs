using UnityEngine;

public class PlayerAimController : MonoBehaviour {
    public enum InputType { Mouse, PS4RightStick }
    public InputType inputType = InputType.Mouse;
    public GameObject crossHair;
    public float sensitivity = 5.0f; // Sensitivity factor
    public float rotationSpeed = 5.0f; // Rotation speed for smoothness

    // Reference to the canvas RectTransform of the crosshair
    private RectTransform crosshairRectTransform;

    private float initialXRotation;

    void Start() {
        // Get the RectTransform of the crosshair
        crosshairRectTransform = crossHair.GetComponent<RectTransform>();

        // Store the initial X rotation of the player
        initialXRotation = transform.rotation.eulerAngles.x;
    }

    void Update() {
        // Check if the input type is Mouse
        if (inputType == InputType.Mouse) {
            // Calculate the position for the crosshair based on the mouse position
            Vector3 mousePosition = Input.mousePosition;
            crosshairRectTransform.position = mousePosition;

            // Convert the mouse position to a world point
            Vector3 targetWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 10f));

            // Calculate the aim direction
            Vector3 aimDirection = targetWorldPosition - transform.position;
            aimDirection.y = 0f; // Keep the player upright

            // Calculate the Y rotation (yaw) based on the aim direction
            float yRotation = Mathf.Atan2(aimDirection.x, aimDirection.z) * Mathf.Rad2Deg;

            // Create the target rotation
            Quaternion targetRotation = Quaternion.Euler(initialXRotation, yRotation, transform.rotation.eulerAngles.z);

            // Smoothly interpolate between the current rotation and the target rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
