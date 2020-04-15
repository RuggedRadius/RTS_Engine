using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Interfaces;

public class Unit_GroundRanged : MonoBehaviour, IMovable, IAttacking
{
    [SerializeField]
    Unit unit;

    [SerializeField]
    private GameObject muzzleFlashPrefab;

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
        GameObject muzzleFlash = Instantiate(muzzleFlashPrefab);
        muzzleFlash.transform.SetParent(this.transform);
        muzzleFlash.transform.localPosition = new Vector3(0.67f, 1.16f, 1.8f);
    }

    public void attack(Vector3 position)
    {
        GameObject muzzleFlash = Instantiate(muzzleFlashPrefab);
        muzzleFlash.transform.SetParent(this.transform);
        muzzleFlash.transform.localPosition = new Vector3(0.67f, 1.16f, 1.8f);
    }

    public void stopAttack()
    {

    }
}
