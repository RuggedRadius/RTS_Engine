using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAttackRange : MonoBehaviour
{
    [SerializeField]
    private float range;

    [SerializeField]
    public List<GameObject> inRangeEnemies;

    [SerializeField]
    public LayerMask enemyScanLayer;

 
    private Unit unit;



    void Start()
    {
        unit = this.GetComponent<Unit>();

        SphereCollider rangeSphere = this.gameObject.AddComponent<SphereCollider>();
        rangeSphere.isTrigger = true;
        rangeSphere.radius = range;

        StartCoroutine(scanForEnemies());
    }



    public IEnumerator scanForEnemies()
    {
        RaycastHit hitScan;
        Ray ray;
        Vector3 rayOrigin;
        Vector3 rayDirection;
        int rayCount = 180;
        while (unit.GetComponent<UnitLife>().alive)
        {
            inRangeEnemies.Clear();


            for (int i = 0; i <= rayCount; i++)
            {
                rayOrigin = this.transform.position + Vector3.up;
                rayDirection = Vector3.Lerp(-transform.right + transform.forward, transform.right + transform.forward, (float)i/(float)rayCount);
                
                ray = new Ray(rayOrigin, rayDirection);
                //Debug.DrawRay(rayOrigin, rayDirection * range, Color.yellow, 1f);

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
}
