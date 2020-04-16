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

    [SerializeField]
    private UnitAttackRange attackRange;

    public void Start()
    {
        attackRange = GetComponent<UnitAttackRange>();
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
        StartCoroutine(attackUnitRoutine(target));
    }

    public IEnumerator attackUnitRoutine(GameObject target)
    {

        StopAllCoroutines();

        Unit targetUnit = target.GetComponent<Unit>();

        if (targetUnit != null)
        {
            while (targetUnit.GetComponent<UnitLife>().lifeCurrent > 0)
            {
                // Get in range
                while (!attackRange.inRangeEnemies.Contains(target))
                {
                    print("moving to tagrte");
                    // Move to target
                    unit.move(target.transform.position);

                    // Look at target
                    this.transform.LookAt(target.transform);

                    yield return null;
                }
                print("stoppping..");
                // Stop moving
                unit.stopMoving();

                // Look at target
                this.transform.LookAt(target.transform);

                // Create muzzle flash
                GameObject muzzleFlash = Instantiate(muzzleFlashPrefab);
                muzzleFlash.transform.SetParent(this.transform);
                muzzleFlash.transform.localPosition = new Vector3(0.67f, 1.16f, 1.8f);

                // SFX
                // ...

                print("Attacking " + target.name);

                // Target take damage
                targetUnit.GetComponent<UnitLife>().TakeDamage((int)unit.unitBaseDamage);

                //yield return null;
            }
        }



        yield return null;
    }

    //public void attack(Vector3 position)
    //{
    //    GameObject muzzleFlash = Instantiate(muzzleFlashPrefab);
    //    muzzleFlash.transform.SetParent(this.transform);
    //    muzzleFlash.transform.localPosition = new Vector3(0.67f, 1.16f, 1.8f);
    //}

    public void stopAttack()
    {

    }
}
