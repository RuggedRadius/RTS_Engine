using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Interfaces;

public class Unit_Air : MonoBehaviour, IMovable, IAttacking
{
    [SerializeField]
    Unit unit;

    public void attack(GameObject target)
    {

    }

    public void attack(Vector3 position)
    {

    }

    public void stopAttack()
    {

    }

    public void move(Vector3 destination)
    {
        unit.agent.SetDestination(destination);
    }

    public void stopMoving()
    {
        unit.agent.SetDestination(unit.agent.nextPosition);
    }
}
