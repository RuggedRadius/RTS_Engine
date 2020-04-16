using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAttackRange : MonoBehaviour
{
    [SerializeField]
    private float range;

    [SerializeField]
    public List<GameObject> inRangeEnemies;

 
    private Unit unit;



    void Start()
    {
        unit = this.GetComponent<Unit>();

        SphereCollider rangeSphere = this.gameObject.AddComponent<SphereCollider>();
        rangeSphere.isTrigger = true;
        rangeSphere.radius = range;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            // Something on units layer entered 
            if (other.gameObject.GetComponent<Unit>() != null)
            {
                
                // Unit entered
                Unit enteringUnit = other.transform.GetComponent<Unit>();

                if (enteringUnit != null)
                {
                    if (enteringUnit.team != unit.team)
                    {
                        if (!inRangeEnemies.Contains(other.gameObject))
                        {
                            // Enemy entered range
                            inRangeEnemies.Add(other.gameObject);
                        }
                    }
                    else
                    {
                        // Friendly unit entered range
                    }
                }
            }
            else
            {
                Debug.LogError("Couldnt find unit script");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.root.GetComponent<Unit>() != null)
        {
            if (inRangeEnemies.Contains(other.gameObject))
            {
                inRangeEnemies.Remove(other.gameObject);
            }
        }
    }
}
