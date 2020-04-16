using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static Interfaces;

public class Unit_GroundMelee : MonoBehaviour, IMovable, IAttacking
{
    [SerializeField]
    Unit unit;

    private void Start()
    {
        unit = this.GetComponent<Unit>();
    }

    public void move(Vector3 destination)
    {
        unit.agent.SetDestination(destination);
    }

    public void stopMoving()
    {
        unit.agent.SetDestination(unit.agent.nextPosition);
    }

    public void attack(GameObject target)
    {
        print("Attacking " + target.name);

        this.transform.LookAt(target.transform);

    }

    public void attack(Vector3 position) 
    {

    }

    public void stopAttack()
    {

    }
}
