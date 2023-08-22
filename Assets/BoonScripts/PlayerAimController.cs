using UnityEngine;

public class PlayerAimController : MonoBehaviour {
    public float rotationSpeed = 5.0f;

    private float initialXRotation;

    PlayerController player;

    void Start() {
        // Store the initial X rotation of the player
        initialXRotation = transform.localRotation.eulerAngles.x;
        player = GetComponent<PlayerController>();
    }

    void Update() {
        
        if (player.player.ToString() == "P1") {
            // Get the mouse position as a world point
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10; // Set the distance from the camera

            // Convert the mouse position to a world point
            Vector3 targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            // Calculate the target rotation to look at the mouse position while preserving the initial X rotation
            Vector3 targetDirection = targetPosition - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

            targetRotation.eulerAngles = new Vector3(initialXRotation, targetRotation.eulerAngles.y, targetRotation.eulerAngles.z);

            // Smoothly interpolate between the current rotation and the target rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
        
        
       
    }
}
