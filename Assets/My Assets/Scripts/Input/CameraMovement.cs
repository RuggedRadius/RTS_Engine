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
    [SerializeField]
    private Camera camMinimap;

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


    float inputX;
    float inputY;
    private void getKeyboardInputs()
    {
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");
        focalPoint.transform.position += -inputY * focalPoint.transform.forward * keyboardMovementSpeed;
        focalPoint.transform.position += -inputX * focalPoint.transform.right * keyboardMovementSpeed;

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
        Debug.DrawRay(ray.origin, ray.direction, Color.red, 3f, false);

        if (Physics.Raycast(ray, out RaycastHit hit, 10000f, heightGatherLayerMask, QueryTriggerInteraction.Ignore))
        {
            // Hit terrain
            return hit.point.y;
        }
        else
        {
            Debug.LogWarning("No terrain below!");

            if (Physics.Raycast(ray, out hit))
            {
                print("Hit " + hit.collider.name + " instead on layer " + hit.collider.gameObject.layer);
            }
            return -0f;
        }
    }
    private void confineFocalPointToTerrainBounds()
    {
        focalX = focalPoint.transform.position.x;
        focalZ = focalPoint.transform.position.z;

        // Y-Axis
        //focalY = getHeightFromPosition(focalPoint.transform.position);
        focalY = 0;
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

        // Smoothly apply new position
        Vector3 velocity = Vector3.zero;
        cam.transform.localPosition = Vector3.SmoothDamp(cam.transform.localPosition, targetPosition, ref velocity, 0.05f);
    }

}
