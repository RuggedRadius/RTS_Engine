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

public enum UnitType
{
    GroundMelee,
    GroundRanged,
    Air,
    AirTransport
}

[Serializable]
public class Unit : MonoBehaviour, IMovable
{
    #region Properties
    [Header("Unit Information")]
    [SerializeField]
    public string unitName;
    public UnitType unitType;
    public Team team;

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
    
    public NavMeshAgent agent;
    #endregion

    #region MonoBehaviour
    private void Awake()
    {
        InitialiseNavMashAgent();
        unitLife = this.gameObject.GetComponent<UnitLife>();
    }

    private void Start()
    {
        team = this.transform.root.GetComponent<TeamScript>().team;

    }

    private void Update()
    {

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

        switch (unitType)
        {
            case UnitType.GroundMelee:
                this.GetComponent<Unit_GroundMelee>().move(destination);
                break;

            case UnitType.Air:
                this.GetComponent<Unit_Air>().move(destination);
                break;

            case UnitType.GroundRanged:
                this.GetComponent<Unit_GroundRanged>().move(destination);
                break;

            case UnitType.AirTransport:
                //this.GetComponent<Unit>().move(destination);
                break;
        }

        //agent.SetDestination(destination);

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

    public void updateAnimatorValues()
    {
        if (anim != null)
        {
            anim.SetBool("Moving", moving);
        }
        else
        {
            Debug.LogWarning("No animator set for unit: " + this.gameObject.name);
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

    public void Attack(GameObject target)
    {
        switch (unitType)
        {
            case UnitType.GroundMelee:
                this.GetComponent<Unit_GroundMelee>().attack(target);
                break;

            case UnitType.Air:
                this.GetComponent<Unit_Air>().attack(target);
                break;

            case UnitType.GroundRanged:
                this.GetComponent<Unit_GroundRanged>().attack(target);
                break;
        }
    }
}
