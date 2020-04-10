using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private GameObject focalPoint;
    [SerializeField]
    private Terrain terrain;

    [Header("Camera Settings")]
    [SerializeField]
    public float camHeightCur;
    [SerializeField]
    private float camHeightMin;
    [SerializeField]
    private float camHeightMax;
    [SerializeField]
    private float camDistance;
    [SerializeField]
    private LayerMask heightGatherLayerMask;

    [Header("Speeds")]
    [SerializeField]
    private float keyboardMovementSpeed;
    [SerializeField]
    private float scrollSpeedMin;
    [SerializeField]
    private float scrollSpeedMax;
    [SerializeField]
    private float scrollSpeedCur;
    [SerializeField]
    private float camRotationSpeed;
    [SerializeField]
    private float zoomSpeedMin;
    [SerializeField]
    private float zoomSpeedMax;
    [SerializeField]
    private float zoomSpeedCur;

    private float focalX;
    private float focalY;
    private float focalZ;

    private void Start()
    {
        camHeightCur = camHeightMax;
    }

    void Update()
    {
        // Adjust scroll/zoom speeds
        scrollSpeedCur = Mathf.Lerp(scrollSpeedMin, scrollSpeedMax, camHeightCur / camHeightMax);
        keyboardMovementSpeed = scrollSpeedCur;

        // Get inputs
        getMouseZoom();
        getKeyboardInputs();

        // Confine and position camera
        confineFocalPointToTerrainBounds();
        positionCamera();

        // Look at focal point
        cam.transform.LookAt(focalPoint.transform);
    }

    private void getMouseZoom()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            zoomSpeedCur = calcZoomSpeed();
            camHeightCur -= Input.mouseScrollDelta.y * zoomSpeedCur;
            positionCamera();
        }
    }

    private float calcZoomSpeed()
    {
        return Mathf.Lerp(zoomSpeedMin, zoomSpeedMax, camHeightCur / camHeightMax);
    }

    private void getKeyboardInputs()
    {
        if (Input.GetKey(KeyCode.W))
        {
            focalPoint.transform.position += -focalPoint.transform.forward * keyboardMovementSpeed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            focalPoint.transform.position += focalPoint.transform.right * keyboardMovementSpeed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            focalPoint.transform.position += focalPoint.transform.forward * keyboardMovementSpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            focalPoint.transform.position += -focalPoint.transform.right * keyboardMovementSpeed;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            focalPoint.transform.eulerAngles += Vector3.up * camRotationSpeed;
        }
        if (Input.GetKey(KeyCode.E))
        {
            focalPoint.transform.eulerAngles += Vector3.down * camRotationSpeed;
        }
    }
    private float getHeightFromPosition(Vector3 position)
    {
        Vector3 modifiedPosition = new Vector3(position.x, 1000f, position.z);
        Ray ray = new Ray(modifiedPosition, -transform.up);
        Debug.DrawRay(ray.origin, ray.direction, Color.red, 3f);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            //Debug.Log("Hit " + hit.collider.gameObject.name + " at " + hit.point);

            // Hit terrain
            return hit.point.y;
        }
        else
        {
            Debug.LogError("No terrain below!");
            return -100f;
        }
               
    }
    private void confineFocalPointToTerrainBounds()
    {
        focalX = focalPoint.transform.position.x;
        focalZ = focalPoint.transform.position.z;

        //// Limit X-Axis
        //if (focalPoint.transform.position.x < terrain.transform.position.x)
        //{
        //    Debug.Log("Below X Min");
        //    focalX = terrain.transform.position.x; // X value up
        //    focalZ = focalPoint.transform.position.z;
        //}
        //if (focalPoint.transform.position.x > terrain.transform.position.x + terrain.terrainData.size.x)
        //{
        //    Debug.Log("Above X Max");
        //    focalX = terrain.transform.position.x + terrain.terrainData.size.x; // X value down
        //    focalZ = focalPoint.transform.position.z;
        //}

        //// Limit Z-Axis
        //if (focalPoint.transform.position.z < terrain.transform.position.z)
        //{
        //    Debug.Log("Below Z Min");
        //    focalX = focalPoint.transform.position.x;
        //    focalZ = terrain.transform.position.z; // Z Axis up
        //}
        //if (focalPoint.transform.position.z > terrain.transform.position.z + terrain.terrainData.size.z)
        //{
        //    Debug.Log("Above Z Max");
        //    focalX = focalPoint.transform.position.x;
        //    focalZ = terrain.transform.position.z + terrain.terrainData.size.z; // Z Axis Down
        //}

        // Y-Axis
        focalY = getHeightFromPosition(focalPoint.transform.position);
        //focalY += 0.5f;

        // Set focal point position
        focalPoint.transform.position = new Vector3(focalX, focalY, focalZ);
    }
    private void positionCamera()
    {
        // Clamp cam height
        camHeightCur = Mathf.Clamp(camHeightCur, camHeightMin, camHeightMax);

        // Determine camera position based on rotation, Set camera position
        Vector3 targetPosition = (Vector3.forward * camDistance) + (Vector3.up * (camHeightCur));

        //// Get height at proposed position and make sure it is above terrain height
        //float curTerrainHeight = getHeightFromPosition(cam.transform.position + targetPosition);
        //float offset = 100;
        //if (targetPosition.y <= curTerrainHeight)
        //{
        //    targetPosition = new Vector3(
        //        targetPosition.x,
        //        curTerrainHeight - offset,
        //        targetPosition.z
        //        );
        //}

        // Smoothly apply new position
        Vector3 velocity = Vector3.zero;
        cam.transform.localPosition = Vector3.SmoothDamp(cam.transform.localPosition, targetPosition, ref velocity, 0.05f);
    }

}
