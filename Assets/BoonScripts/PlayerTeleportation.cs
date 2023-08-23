using UnityEngine;

public class PlayerTeleportation : MonoBehaviour {
    public Transform targetTeleporter; // The other teleporter to teleport to.
    public float detectionRadius = 2.0f; // Radius within which the player can be detected.
    public bool onTeleporter = false;

    [SerializeField] CharacterController player;

    private void OnTriggerEnter(Collider other) {
        Debug.Log(other.gameObject);
        if (other.CompareTag("Player")) 
        {
            player = other.GetComponent<CharacterController>();

            onTeleporter = true;
        }                    

    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            onTeleporter = false;
        }
    }
    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.E) && onTeleporter) {

           
            TeleportPlayer();
        } 
         
                   
    }

    

    public void TeleportPlayer() {
            
    
        if (player != null && onTeleporter) {
      
            // Teleport the player to the target teleporter's position.
            player.GetComponent<CharacterController>().enabled = false;
            player.gameObject.transform.position = new Vector3(targetTeleporter.position.x, player.transform.position.y,targetTeleporter.position.z);
            player.GetComponent<CharacterController>().enabled = true;
        }
    }
}
