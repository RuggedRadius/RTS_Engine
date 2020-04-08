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

    private NavMeshAgent agent;

    void Start()
    {
        agent = this.gameObject.AddComponent<NavMeshAgent>();
    }

    public void move(Vector3 destination)
    {      
        agent.SetDestination(destination);        
    }
}
