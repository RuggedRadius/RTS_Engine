using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using static Interfaces;

[Serializable]
public class Unit : MonoBehaviour
{
    #region Properties
    [Header("Unit Information")]
    public string unitName;
    public UnitType unitType;
    public Team team;
    public List<ResourceCost> resourceCosts;
    public float buildTime;

    [Header("Prefab Resources")]
    public GameObject unitPrefab;
    public Sprite uiTileSprite;



    [Header("References")]
    [SerializeField]
    private Animator anim;
    public Image worldLifeBarPanel;

    [Header("Settings")]
    [SerializeField]
    private float destinationTolerance;

    [Header("State")]
    public bool moving;



    private GameManager gm;
    private TeamManager tm;

    //[HideInInspector]
    public Unit_Life life;
    //[HideInInspector]
    public Unit_Statistics stats;
    //[HideInInspector]
    public Unit_Attack attack;
    //[HideInInspector]
    public NavMeshAgent agent;
    //[HideInInspector]
    public Unit_Movement movement;

    #endregion

    private void Awake()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        tm = gm.GetComponentInChildren<TeamManager>();

        life = this.GetComponent<Unit_Life>();
        stats = this.gameObject.GetComponent<Unit_Statistics>();
        attack = this.gameObject.GetComponent<Unit_Attack>();
        movement = this.gameObject.GetComponent<Unit_Movement>();


    }

    private void Start()
    {
        team = this.transform.root.GetComponent<TeamScript>().team;
        agent = this.gameObject.GetComponent<NavMeshAgent>();

        stats = this.GetComponent<Unit_Statistics>();
    }

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
}
