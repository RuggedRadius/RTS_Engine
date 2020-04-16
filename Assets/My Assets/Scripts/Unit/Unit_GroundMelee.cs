using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static Interfaces;

public class Unit_GroundMelee : MonoBehaviour, IMovable, IAttacking
{
    private GameManager gm;
    private SelectionManager sm;

    Unit unit;
    private UnitAttackRange attackRange;

    private void Start()
    {
        unit = this.GetComponent<Unit>();
        attackRange = GetComponent<UnitAttackRange>();
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        sm = gm.GetComponentInChildren<SelectionManager>();
    }

    public void Update()
    {
        LifeBarDisplayIfSelected();
    }

    private void LifeBarDisplayIfSelected()
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
        if (unit != null)
        {
            if (unit.agent != null)
            {
                unit.agent.SetDestination(destination);
            }
            else
            {
                Debug.LogError("No navmesh agent found.");
            }    
        }
        else
        {
            Debug.LogError("No unit script found.");
        }
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

    public void attack(Vector3 position) 
    {

    }

    public void stopAttack()
    {

    }
}
