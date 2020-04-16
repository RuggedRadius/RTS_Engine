using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Attack : MonoBehaviour
{
    [SerializeField]
    private float range;

    [SerializeField]
    public List<GameObject> inRangeEnemies;

    [SerializeField]
    public LayerMask enemyScanLayer;

 
    private Unit unit;

    private void Awake()
    {
        unit = this.GetComponent<Unit>();
    }

    void Start()
    {
        StartCoroutine(scanForEnemies());
    }

    public IEnumerator scanForEnemies()
    {
        RaycastHit hitScan;
        Ray ray;
        Vector3 rayOrigin;
        Vector3 rayDirection;
        int rayCount = 180;
        while (unit.life.alive)
        {
            inRangeEnemies.Clear();


            for (int i = 0; i <= rayCount; i++)
            {
                rayOrigin = this.transform.position + Vector3.up;
                rayDirection = Vector3.Lerp(-transform.right + transform.forward, transform.right + transform.forward, (float)i/(float)rayCount);
                
                ray = new Ray(rayOrigin, rayDirection);
                Debug.DrawRay(rayOrigin, rayDirection * range, Color.yellow, 1f);

                if (Physics.Raycast(ray, out hitScan, range, enemyScanLayer, QueryTriggerInteraction.Ignore))
                {
                    // Unit hit
                    if (hitScan.collider.gameObject.GetComponent<Unit>() != null)
                    {
                        Unit hitUnit = hitScan.collider.gameObject.GetComponent<Unit>();
                        if (hitUnit.team != unit.team)
                        {
                            if (!inRangeEnemies.Contains(hitScan.collider.gameObject))
                            {
                                // Enemy Unit found
                                inRangeEnemies.Add(hitUnit.gameObject);
                            }
                        }
                    }
                }
            }

            yield return new WaitForSeconds(0.5f);
        }
    }

    public void attack(GameObject target)
    {
        StopAllCoroutines();
        StartCoroutine(scanForEnemies());
        StartCoroutine(attackUnitRoutine(target));
    }

    public bool attacking;
    public IEnumerator attackUnitRoutine(GameObject target)
    {
        Unit targetUnit = target.GetComponent<Unit>();

        if (targetUnit != null)
        {
            Unit_Life targetLife = targetUnit.GetComponent<Unit_Life>();

            if (targetLife != null)
            {
                attacking = true;

                while (attacking)
                {
                    //print("Target is alive");

                    // Look at target
                    this.transform.LookAt(target.transform);

                    if (inRangeEnemies.Contains(target))
                    {
                        //print("Target in range, attacking: " + target.name);

                        // Stop moving
                        unit.agent.SetDestination(unit.transform.position);

                        // SFX
                        // ...

                        // Target take damage
                        targetLife.TakeDamage((int)unit.stats.unitBaseDamage);

                        yield return new WaitForSeconds(1);
                    }
                    else
                    {
                        while (attacking && !inRangeEnemies.Contains(target))
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

                            yield return new WaitForSeconds(0.1f);
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
