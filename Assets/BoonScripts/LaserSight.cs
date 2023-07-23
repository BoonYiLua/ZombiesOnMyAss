using UnityEngine;

public class LaserSight : MonoBehaviour {
    public LineRenderer lineRenderer;
    public Transform endPoint; // The point where the laser will end (e.g., the weapon's forward position).
    public float laserWidth = 0.02f; // Adjust this value to change the laser width.

    private bool isLineRendererEnabled = true; // Flag to track if the Line Renderer should be enabled.

    private void Start() {
        // Set the initial width of the line renderer.
        lineRenderer.startWidth = laserWidth;
        lineRenderer.endWidth = laserWidth;
    }

    private void Update() {
        // Get the position of the LaserSight GameObject.
        Vector3 startPosition = transform.position;

        // Calculate the direction from the starting position to the end point.
        Vector3 direction = (endPoint.position - startPosition).normalized;

        // Set the LaserSight GameObject's forward direction to match the calculated direction.
        transform.forward = direction;

        // Set the LineRenderer positions.
        lineRenderer.SetPosition(0, startPosition);

        // Check if the Line Renderer should be enabled or disabled.
        lineRenderer.enabled = isLineRendererEnabled;

        // Cast a ray to detect if the laser hits something.
        RaycastHit hit;
        if (Physics.Raycast(startPosition, direction, out hit)) {
            // If the ray hits something, set the end point of the line renderer to the hit point.
            lineRenderer.SetPosition(1, hit.point);
        } else {
            // If the ray doesn't hit anything, set the end point of the line renderer to the original endpoint.
            lineRenderer.SetPosition(1, endPoint.position);
        }
    }

    // Method to enable the Line Renderer (called from GunController)
    public void EnableLaserSight() {
        isLineRendererEnabled = true;
    }

    // Method to disable the Line Renderer (called from GunController)
    public void DisableLaserSight() {
        isLineRendererEnabled = false;
    }
}
