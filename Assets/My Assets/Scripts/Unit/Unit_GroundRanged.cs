using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Interfaces;

public class Unit_GroundRanged : MonoBehaviour, IMovable, IAttacking
{
    private GameManager gm;
    private SelectionManager sm;

    [SerializeField]
    Unit unit;

    [SerializeField]
    private GameObject muzzleFlashPrefab;

    [SerializeField]
    private UnitAttackRange attackRange;



    public void Start()
    {
        attackRange = GetComponent<UnitAttackRange>();
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        sm = gm.GetComponentInChildren<SelectionManager>();
    }

    public void Update()
    {
        if (sm.currentSelection.Contains(this))
        {
            this.transform.Find("Life Bar").gameObject.SetActive(true);
        }
        else
        {
            this.transform.Find("Life Bar").gameObject.SetActive(false);
        }
    }

    public void move(Vector3 destination)
    {
        StopAllCoroutines();
        unit.agent.SetDestination(destination);
    }

    public void stopMoving()
    {
        StopAllCoroutines();
        unit.agent.SetDestination(unit.agent.nextPosition);
    }

    public void attack(GameObject target)
    {
        StopAllCoroutines();
        StartCoroutine(attackUnitRoutine(target));
    }

    bool attacking;
    public IEnumerator attackUnitRoutine(GameObject target)
    {
        Unit targetUnit = target.GetComponent<Unit>();

        if (targetUnit != null)
        {
            UnitLife targetLife = targetUnit.GetComponent<UnitLife>();

            if (targetLife != null)
            {
                attacking = true;

                while (attacking)
                {
                    //print("Target is alive");

                    // Look at target
                    this.transform.LookAt(target.transform);

                    if (attackRange.inRangeEnemies.Contains(target))
                    {
                        //print("Target in range, attacking: " + target.name);

                        // Stop moving
                        unit.agent.SetDestination(unit.transform.position);

                        // Create muzzle flash
                        GameObject muzzleFlash = Instantiate(muzzleFlashPrefab);
                        muzzleFlash.transform.SetParent(this.transform);
                        muzzleFlash.transform.localPosition = new Vector3(0.67f, 1.16f, 1.8f);
                        //muzzleFlash.transform.localScale = Vector3.one * 10;

                        // SFX
                        // ...

                        // Target take damage
                        targetLife.TakeDamage((int)unit.unitBaseDamage);

                        yield return new WaitForSeconds(1);
                    }
                    else
                    {
                        while (!attackRange.inRangeEnemies.Contains(target))
                        {
                            //print("Moving to target");

                            // Move to target
                            try
                            {
                                unit.agent.SetDestination(target.transform.position);
                            }
                            catch (MissingReferenceException ex)
                            {
                                attacking = false;
                            }

                            yield return new WaitForSeconds(1);
                        }
                    }

                    if (targetLife.lifeCurrent > 0)
                    {
                        //print("Target still alive");
                        attacking = true;
                    }
                    else
                    {
                        //print("Target dead.");
                        attacking = false;
                    }
                }
            }
            else
            {
                Debug.LogWarning("Couldnt find UnitLife script on attack target: " + targetUnit.name);
            }
        }
        //print("Coroutine complete");
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
