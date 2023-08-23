using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    NavMeshAgent agent;
    Animator anim; 
    public Transform target;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();


    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.position);
        anim.SetFloat("MoveY", agent.velocity.magnitude / agent.speed);

    }
}
