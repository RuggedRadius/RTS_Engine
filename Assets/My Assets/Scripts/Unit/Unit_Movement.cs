using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit_Movement : MonoBehaviour
{
    private NavMeshAgent agent;

    private void Awake()
    {
        agent = this.GetComponent<NavMeshAgent>();
    }

    public void move(Vector3 destination)
    {
        StopAllCoroutines();

        if (agent != null)
        {
            agent.SetDestination(destination);
            this.GetComponent<Unit_Attack>().attacking = false;
        }
        else
        {
            Debug.LogError("No NavMeshAgent found.");
        }
    }

    public void stopMoving()
    {
        StopAllCoroutines();
        agent.SetDestination(agent.nextPosition);
    }
}
