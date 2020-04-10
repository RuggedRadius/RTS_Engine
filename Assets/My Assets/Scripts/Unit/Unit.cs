using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static Interfaces;

[Serializable]
public class Unit : MonoBehaviour, IMovable
{
    #region Properties
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
    #endregion

    #region MonoBehaviour
    void Start()
    {
        agent = this.gameObject.AddComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (moving)
        {
            // Check if close to destination
            checkDestination();
        }
    }
    #endregion

    #region IMovable interface
    public void move(Vector3 destination)
    {
        moving = true;
        agent.SetDestination(destination);

        // Update animator values
        updateAnimatorValues();
    }

    public void stopMoving()
    {
        agent.SetDestination(agent.nextPosition);
        moving = false;

        // Update animator values
        updateAnimatorValues();
    }
    #endregion

    private void updateAnimatorValues()
    {
        anim.SetBool("Moving", moving);
    }
    private void checkDestination()
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
    private Vector3 distanceToDestination(Vector3 _destination)
    {
        return new Vector3( 
            _destination.x - agent.transform.position.x,
            _destination.y - agent.transform.position.y,
            _destination.z - agent.transform.position.z
            );
    }
}
