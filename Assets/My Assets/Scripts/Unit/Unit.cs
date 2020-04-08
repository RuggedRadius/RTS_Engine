using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public class Unit : MonoBehaviour
{
    [Header("Unit Information")]
    [SerializeField]
    public string unitName;

    [Header("Stats")]
    [SerializeField]
    [Range(5f, 20f)]
    public float unitSpeed;
    [SerializeField]
    public float unitDamageBase;
    [SerializeField]
    public float unitBuildTime;

    [Header("Prefab")]
    [SerializeField]
    public GameObject prefab;

    [Header("State")]
    public bool moving;

    [Header("References")]
    [SerializeField]
    private Animator anim;

    [Header("Settings")]
    [SerializeField]
    private float destinationTolerance;
    
    private NavMeshAgent agent;

    void Start()
    {
        agent = this.gameObject.AddComponent<NavMeshAgent>();
    }

    private void Update()
    {
        // Update animator values
        updateAnimatorValues();

        // Check if close to destination
        checkDestinationTolerance();


    }

    public void move(Vector3 destination)
    {
        moving = true;
        agent.SetDestination(destination);
    }

    private void checkDestinationTolerance()
    {
        // If all 3 components of the vector 3 are withinn tolerance
        if (
            distanceToDestination(agent.destination).x < destinationTolerance &&
            distanceToDestination(agent.destination).y < destinationTolerance &&
            distanceToDestination(agent.destination).z < destinationTolerance
            )
        {
            // No more movings
            agent.destination = agent.transform.position;
            moving = false;
        }
    }

    private void updateAnimatorValues()
    {
        anim.SetBool("Moving", moving);
    }

    private Vector3 distanceToDestination(Vector3 _destination)
    {
        return new Vector3( 
            _destination.x - agent.transform.position.x,
            _destination.y - agent.transform.position.y,
            _destination.z - agent.transform.position.z
            );
    }
}
