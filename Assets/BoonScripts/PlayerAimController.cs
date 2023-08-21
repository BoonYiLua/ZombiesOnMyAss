using UnityEngine;

public class PlayerAimController : MonoBehaviour {
    public enum InputType { Mouse, PS4RightStick }
    public InputType inputType = InputType.Mouse;
    public GameObject crossHair;
    public float sensitivity = 5.0f;
    public float rotationSpeed = 5.0f;
    public float smoothFactor = 0.1f;

    private RectTransform crosshairRectTransform;

    private float initialXRotation;
    private Quaternion targetRotation = Quaternion.identity;

    void Start() {
        crosshairRectTransform = crossHair.GetComponent<RectTransform>();
        initialXRotation = transform.rotation.eulerAngles.x;
    }

    void Update() {
        if (inputType == InputType.Mouse) {
            Vector3 mousePosition = Input.mousePosition;
            crosshairRectTransform.position = mousePosition;

            // Create a ray from the camera to the mouse position
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            RaycastHit hit;

            // Check if the ray hits something in the scene
            if (Physics.Raycast(ray, out hit, 100)) {
                // Calculate the target rotation to look at the hit point while preserving the initial X rotation
                Vector3 targetPoint = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
                targetRotation.eulerAngles = new Vector3(initialXRotation, targetRotation.eulerAngles.y, targetRotation.eulerAngles.z);
            }

            // Smoothly interpolate between the current rotation and the target rotation with the smoothFactor
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }
}
