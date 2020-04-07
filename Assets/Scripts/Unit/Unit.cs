using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    [Header("Unit Information")]
    [SerializeField]
    private string unitName;

    [Header("Stats")]
    [SerializeField]
    [Range(5f, 20f)]
    private float unitSpeed;
    [SerializeField]
    private float unitDamageMin;

    [SerializeField]
    private UnitSelection unitSelection;

    private NavMeshAgent agent;

    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        
    }

    public void move(Vector3 destination)
    {
        Debug.Log("Moving " + unitName + " to " + destination);        
        agent.SetDestination(destination);
    }

    private void OnMouseDown()
    {
        Debug.Log("Mouse clicked " + unitName);
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            unitSelection.selectedUnits.Add(this);
        }
        else
        {
            unitSelection.selectedUnits.Clear();
            unitSelection.selectedUnits.Add(this);
        }
    }

    private void OnMouseEnter()
    {
        Debug.Log("Mouse over " + unitName);
    }
}
