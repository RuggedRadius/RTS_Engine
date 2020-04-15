using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCamera : MonoBehaviour
{
    [SerializeField]
    private PlaneSurfaceOriginal plane;
    [SerializeField]
    private float rotationSpeed;

    float xPos;
    float yPos;
    float zPos;

    private Transform center;

    // Start is called before the first frame update
    void Start()
    {
        PositonCamera();

        // Create center point
        GameObject centerPoint = new GameObject();
        xPos = (plane.width * 100) / 2;
        yPos = 0f;
        zPos = (plane.length * 100) / 2;
        centerPoint.transform.position = new Vector3(xPos, yPos, zPos);
        center = centerPoint.transform;

        this.transform.LookAt(center);
    }

    // Update is called once per frame
    void Update()
    {

        this.transform.RotateAround(center.position, Vector3.up, Time.deltaTime * rotationSpeed);
    }

    private void PositonCamera()
    {
        // Position Camera
        xPos = (plane.width * 100) / 2;
        yPos = xPos * 1.5f;
        zPos = -(xPos / 10);
        this.transform.position = new Vector3(xPos, yPos, zPos);
    }
}
