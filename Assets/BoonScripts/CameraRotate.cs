using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraRotate : MonoBehaviour {
    public float rotationSpeed = 30f; // Adjust this value to control the rotation speed

    private void Update() {
        // Rotate the GameObject around its up (Y) axis
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
