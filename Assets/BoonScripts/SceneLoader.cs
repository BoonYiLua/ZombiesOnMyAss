using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
    public string sceneToLoad; // The name of the scene to load when triggered.

    // This method is called when another Collider enters this GameObject's trigger collider.
    private void OnTriggerEnter(Collider other) {
        // Check if the Collider that entered is tagged as "Player" (or use another method to identify the player).
        if (other.CompareTag("Player")) {
            // Load the specified scene.
            SceneManager.LoadScene("Win");
        }
    }
}