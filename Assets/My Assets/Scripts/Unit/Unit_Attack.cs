using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum AttackState
{
    Passive,
    Agressive
}

public class Unit_Attack : MonoBehaviour
{
    [Header("Range")]
    [SerializeField]
    private float range;
    [SerializeField]
    public List<GameObject> inRange;
    [SerializeField]
    public LayerMask enemyScanLayer;

    [Header("State")]
    [SerializeField]
    private AttackState state;
    public bool attacking;
    public bool attackMoving;

    //[Space(20)]
    [Header("Unit Specifics - Ranged")]
    public GameObject muzzleFlashPrefab;
    public Transform muzzleFlashLocation;

    private Unit unit;
    private NavMeshAgent agent;

    private void Awake()
    {
        unit = this.GetComponent<Unit>();
        agent = this.GetComponent<NavMeshAgent>();
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
            inRange.Clear();


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
                            if (!inRange.Contains(hitScan.collider.gameObject))
                            {
                                // Enemy Unit found
                                inRange.Add(hitUnit.gameObject);
                            }
                        }
                    }
                }
            }

            yield return new WaitForSeconds(0.5f);
        }
    }
    
    public IEnumerator attackRoutine(GameObject target)
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

                    if (inRange.Contains(target))
                    {
                        //print("Target in range, attacking: " + target.name);

                        // Stop moving
                        unit.agent.SetDestination(unit.transform.position);

                        // SFX
                        // ...

                        // UnitType specifics
                        switch (unit.unitType)
                        {
                            case UnitType.GroundRanged:
                                CreateMuzzleFlash();
                                break;
                        }
                        

                        // Target take damage
                        targetLife.TakeDamage((int)unit.stats.unitBaseDamage);

                        yield return new WaitForSeconds(1);
                    }
                    else
                    {
                        while (attacking && !inRange.Contains(target))
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
    public IEnumerator attackRoutine(Vector3 worldPosition)
    {
        state = AttackState.Agressive;
        attackMoving = true;

        while (attackMoving && transform.position != agent.destination)
        {
            // Move to destination
            unit.movement.move(worldPosition);
            
            // Something enemy in range
            if (inRange.Count > 0)
            {
                // Attack it
                CreateMuzzleFlash();
                attack(inRange[0]);

                // Proceed with attack move
                unit.movement.move(worldPosition);
            } 

            if (transform.position != agent.destination)
            {
                print("Attack Moving");
                attackMoving = true;
            }
            else
            {
                print("Reached destination of Attack Move");
                attackMoving = false;
            }
        }
        yield return null;
    }



    public void attack(GameObject target)
    {
        //StopAllCoroutines();
        //StartCoroutine(scanForEnemies());
        StartCoroutine(attackRoutine(target));


    }
    public void attack(Vector3 position)
    {
        StartCoroutine(attackRoutine(position));
    }

    public void stopAttack()
    {

    }


    // Ranged Unit Methods
    private void CreateMuzzleFlash()
    {
        Instantiate(muzzleFlashPrefab, muzzleFlashLocation.position, Quaternion.Euler(Vector3.zero), this.transform);
    }

}
