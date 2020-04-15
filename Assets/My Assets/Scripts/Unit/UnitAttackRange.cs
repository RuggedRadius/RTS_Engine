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
            print(other.gameObject.name);

            GameObject collider = other.gameObject;
            Unit unit = null;
            while (unit == null && collider.transform.parent != null)
            {
                collider = collider.transform.parent.gameObject;
                unit = collider.GetComponent<Unit>();
            }
            
            if (collider.GetComponent<Unit>() != null)
            {
                // Unit entered
                Unit enteringUnit = other.transform.parent.GetComponent<Unit>();

                if (enteringUnit.team != unit.team)
                {
                    // Enemy entered range
                    inRangeEnemies.Add(other.gameObject);
                }
            }
            else
            {
                print("Couldnt find unit script");
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
