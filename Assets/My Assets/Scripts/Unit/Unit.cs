using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static Interfaces;

[Serializable]
public class ResourceCost
{
    [SerializeField]
    public ResourceType type;
    [SerializeField]
    public int cost;
}

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
    public float unitBaseDamage;
    [SerializeField]
    public float unitBuildTime;
    [SerializeField]
    public float unitBaseArmour;
    [SerializeField]
    public float unitAcceleration;
    [SerializeField]
    public float unitRadius;
    [SerializeField]
    public float unitHeight;
    [SerializeField]
    public float unitStoppingDistance;




    [SerializeField]
    public List<ResourceCost> resourceCosts;

    [Header("Prefab Resources")]
    [SerializeField]
    public GameObject unitPrefab;
    [SerializeField]
    public Sprite uiTileSprite;

    [Header("State")]
    public bool moving;

    [Header("References")]
    [SerializeField]
    private Animator anim;
    public UnitLife unitLife;

    [Header("Settings")]
    [SerializeField]
    private float destinationTolerance;
    
    private NavMeshAgent agent;
    #endregion

    #region MonoBehaviour
    private void Awake()
    {
        unitLife = this.gameObject.GetComponent<UnitLife>();
    }
    void Start()
    {
        InitialiseNavMashAgent();
    }

    private void Update()
    {
        if (moving)
        {

        }
    }
    #endregion

    private void InitialiseNavMashAgent()
    {
        agent = this.gameObject.GetComponent<NavMeshAgent>();
        agent.speed = unitSpeed;
        agent.acceleration = unitAcceleration;
        agent.stoppingDistance = unitStoppingDistance;
        agent.radius = unitRadius;
        agent.height = unitHeight;
    }

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
        if (anim != null)
        {
            anim.SetBool("Moving", moving);
        }
        else
        {
            Debug.LogError("No animator set for unit: " + this.gameObject.name);
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
