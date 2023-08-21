using UnityEngine;

public class VisionCone : MonoBehaviour {
    public float viewDistance = 10f;        // How far the cone can "see"
    [Range(1f, 180f)]
    public float viewAngle = 60f;           // The cone's field of view angle (1-180 degrees)
    public Material visionMaterial;         // The material for the vision cone

    private MeshRenderer visionRenderer;    // Renderer for the vision cone
    private MeshFilter visionMeshFilter;    // Mesh filter for the vision cone
    private Mesh visionMesh;                // Mesh data for the vision cone

    private void Start() {
        // Create or find a child GameObject to hold the vision cone mesh
        GameObject coneMeshObject = new GameObject("VisionConeMesh");
        coneMeshObject.transform.parent = transform;
        coneMeshObject.transform.localPosition = Vector3.zero;
        coneMeshObject.transform.localRotation = Quaternion.identity;

        // Add the necessary components for rendering
        visionRenderer = coneMeshObject.AddComponent<MeshRenderer>();
        visionMeshFilter = coneMeshObject.AddComponent<MeshFilter>();

        // Create the vision cone mesh
        visionMesh = CreateVisionConeMesh(viewDistance, viewAngle);

        // Assign the material to the mesh renderer
        if (visionMaterial != null)
            visionRenderer.material = visionMaterial;

        // Set the mesh for rendering
        visionMeshFilter.mesh = visionMesh;
    }

    private Mesh CreateVisionConeMesh(float distance, float angle) {
        Mesh mesh = new Mesh();
        int segments = 30; // Number of segments to approximate the circle

        Vector3[] vertices = new Vector3[segments + 2];
        int[] triangles = new int[segments * 3];

        vertices[0] = Vector3.zero; // Cone tip

        float angleIncrement = angle / segments;

        for (int i = 0; i <= segments; i++) {
            float currentAngle = angleIncrement * i;
            float x = Mathf.Sin(Mathf.Deg2Rad * currentAngle) * distance;
            float y = Mathf.Cos(Mathf.Deg2Rad * currentAngle) * distance;
            vertices[i + 1] = new Vector3(x, y, 0);

            if (i < segments) {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        return mesh;
    }
}
