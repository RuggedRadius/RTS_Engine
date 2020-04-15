using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitFlight : MonoBehaviour
{
    RaycastHit hit;
    Ray ray;


    void Start()
    {
        
    }

    void Update()
    {
        Transform model = this.transform;
        Vector3 origin = this.transform.position + (3 * transform.forward) + (100 * transform.up);
        Vector3 direction = -this.transform.up * 1000;
        ray = new Ray(origin, direction);

        Debug.DrawRay(ray.origin, ray.direction, Color.yellow, 1f, true);

        float heightOffset = 0f;

        if (Physics.Raycast(ray, out hit))
        {
            //print("Distance: " + hit.distance);

            //this.transform.Find("Model").Translate(Vector3.up * (hit.point.y - 100));
            this.transform.Find("Model").localPosition = Vector3.zero + (Vector3.up * hit.point.y) + (Vector3.up * heightOffset);
        }
    }
}
