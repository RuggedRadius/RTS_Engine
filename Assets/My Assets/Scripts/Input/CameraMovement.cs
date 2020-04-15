using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Layer Mask")]
    [SerializeField]
    private LayerMask heightGatherLayerMask;

    [Header("Debug Values")]
    [SerializeField]
    public float camHeightCur;
    [SerializeField]
    private float scrollSpeedCur;
    [SerializeField]
    private float zoomSpeedCur;

    [Header("Camera Settings")]
    [SerializeField]
    private float camHeightMin;
    [SerializeField]
    private float camHeightMax;
    [SerializeField]
    private float camDistance;
    [SerializeField]
    private float camRotationSpeed;

    [Header("Scroll")]
    [SerializeField]
    private float scrollSpeedMin;
    [SerializeField]
    private float scrollSpeedMax;

    [Header("Zoom")]
    [SerializeField]
    private float zoomSpeedMin;
    [SerializeField]
    private float zoomSpeedMax;

    private float focalX;
    private float focalY;
    private float focalZ;
    private Camera cam;
    private Camera camMinimap;
    private GameObject focalPoint;

    private void Awake()
    {
        cam = Camera.main;
        focalPoint = GameObject.FindGameObjectWithTag("FocalPoint");
        camMinimap = GameObject.FindGameObjectWithTag("CamMinimap3D").GetComponent<Camera>();

        cam.transform.position = Vector3.zero;
    }

    private void Start()
    {
        camHeightCur = camHeightMax;
    }

    void Update()
    {
        // Adjust scroll/zoom speeds
        scrollSpeedCur = Mathf.Lerp(scrollSpeedMin, scrollSpeedMax, camHeightCur / camHeightMax);

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
        focalPoint.transform.position += -inputY * focalPoint.transform.forward * scrollSpeedCur;
        focalPoint.transform.position += -inputX * focalPoint.transform.right * scrollSpeedCur;

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

            Debug.LogError("No terrain below!");
            return 0f;
        }
    }
    private void confineFocalPointToTerrainBounds()
    {
        focalX = focalPoint.transform.position.x;
        focalZ = focalPoint.transform.position.z;

        // Y-Axis
        //focalY = getHeightFromPosition(focalPoint.transform.position);
        focalY = 0;
        focalY = 0f;
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
