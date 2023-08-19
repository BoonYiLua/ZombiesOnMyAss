using UnityEngine;

public class PlayerAimController : MonoBehaviour {
    public enum InputType { Mouse, PS4RightStick }
    public InputType inputType = InputType.Mouse;
    public GameObject crossHair;
    public float sensitivity = 5.0f;
    public float rotationSpeed = 5.0f;

    private RectTransform crosshairRectTransform;
    private float initialXRotation;

    void Start() {
        crosshairRectTransform = crossHair.GetComponent<RectTransform>();
        initialXRotation = transform.rotation.eulerAngles.x;
    }

    void Update() {
        if (inputType == InputType.Mouse) {
            Vector3 mousePosition = Input.mousePosition;
            crosshairRectTransform.position = mousePosition;

            // Convert the mouse position to a world point at a fixed distance (e.g., 10 units)
            Vector3 targetWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 10f));

            // Use Transform.LookAt to directly face the crosshair's world position
            transform.LookAt(targetWorldPosition);

            // To keep the player upright, reset the X rotation to the initial value
            Vector3 currentRotation = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(initialXRotation, currentRotation.y, currentRotation.z);
        }
    }
}
