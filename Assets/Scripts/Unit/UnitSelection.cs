using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelection : MonoBehaviour
{
    [SerializeField]
    public List<Unit> selectedUnits;

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private GameObject targetLocationPrefab;

    private Vector3 lastTargetLocation;
    private GameObject lastTargetLocationObject;
    private Ray targetRay;

    void Start()
    {
        selectedUnits = new List<Unit>();


    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            targetRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(targetRay, out RaycastHit hit))
            {
                // Hit terrain
                if (hit.collider.gameObject.layer == 8)
                {
                    // Set location at hit point
                    lastTargetLocation = hit.point;

                    // Move selected units to location
                    moveSelectedUnitsTo(lastTargetLocation);

                    // Add fx to location
                    if (lastTargetLocationObject != null)
                    {
                        Destroy(lastTargetLocationObject);
                    }
                    lastTargetLocationObject = Instantiate(targetLocationPrefab, this.transform);
                    lastTargetLocation = new Vector3(lastTargetLocation.x, lastTargetLocation.y + 0.25f, lastTargetLocation.z);
                    lastTargetLocationObject.transform.position = lastTargetLocation;
                }
            }
        }
    }

    private void moveSelectedUnitsTo(Vector3 _destination)
    {
        foreach (Unit unit in selectedUnits)
        {
            unit.move(_destination);
        }
    }
}
