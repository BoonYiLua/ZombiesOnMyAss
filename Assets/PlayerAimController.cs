using UnityEngine;

public class PlayerAimController : MonoBehaviour {
    public enum InputType { Mouse, PS4RightStick }
    public InputType inputType = InputType.Mouse;

    public float sensitivity = 1f; // Adjust this to control the rotation speed

    private Vector2 inputRotation;

    void Update() {
        // Get the input rotation values based on the chosen input type
        switch (inputType) {
            case InputType.Mouse:
                inputRotation.x = Input.GetAxis("Mouse X") * sensitivity;
                break;
            case InputType.PS4RightStick:
                inputRotation.x = Input.GetAxis("RightStickHorizontal") * sensitivity;
                break;
        }

        // Apply rotation only along the y-axis while keeping x and z fixed
        Vector3 newRotation = transform.eulerAngles;
        newRotation.y -= inputRotation.x;
        transform.eulerAngles = newRotation;
    }
}
