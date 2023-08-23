using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour {
    NavMeshAgent agent;
    Animator anim;
    public List<Transform> targets; // Use a list to store multiple targets

    void Start() {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        // Find and set all player GameObjects with the "Player" tag as targets
        FindAllPlayers();
    }

    // Update is called once per frame
    void Update() {
        if (targets.Count > 0) {
            // Find the closest target among all available targets
            Transform closestTarget = FindClosestTarget();

            if (closestTarget != null) {
                agent.SetDestination(closestTarget.position);
            
            }
        }
    }

    // Method to find all player GameObjects and add them to the targets list
    void FindAllPlayers() {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players) {
            targets.Add(player.transform);
        }
    }

    // Method to find the closest target among all available targets
    Transform FindClosestTarget() {
        Transform closestTarget = null;
        float closestDistance = Mathf.Infinity;

        foreach (Transform targetTransform in targets) {
            float distance = Vector3.Distance(transform.position, targetTransform.position);
            if (distance < closestDistance) {
                closestDistance = distance;
                closestTarget = targetTransform;
            }
        }

        return closestTarget;
    }
}
