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

    [Header("Settings")]
    [SerializeField]
    public float camHeightCur;
    [SerializeField]
    public float camHeightMin;
    [SerializeField]
    public float camHeightMax;


    [SerializeField]
    private float camDistance;

    [SerializeField]
    private float camAngle;

    [SerializeField]
    private float keyboardMovementSpeed;

    [SerializeField]
    float scrollSpeedMin;
    [SerializeField]
    float scrollSpeedMax;
    [SerializeField]
    float scrollSpeedCur;

    [SerializeField]
    private float camRotationSpeed;

    [SerializeField]
    private float camZoomSpeed;

    float camX;
    float camY;
    float camZ;

    void Start()
    {
        positionCamera();
    }

    void Update()
    {
        getKeyboardInputs();
        getMouseScroll();



        scrollSpeedCur = Mathf.Lerp(scrollSpeedMin, scrollSpeedMax, camHeightCur/camHeightMax);
        keyboardMovementSpeed = scrollSpeedCur;


        confineCameraPosition();
        positionCamera();
        cam.transform.LookAt(focalPoint.transform);
    }

    private void getMouseScroll()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            camHeightCur -= Input.mouseScrollDelta.y * camZoomSpeed;
            positionCamera();
        }
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

    private void positionCamera()
    {
        // Determine camera position based on rotation
        //Vector3 camPosition = focalPoint.transform.position + (transform.forward * camDistance) + (transform.up * camHeight);
        Vector3 camPosition = (Vector3.forward * camDistance) + (Vector3.up * (camHeightCur));

        // Set camera position
        cam.transform.localPosition = camPosition;
    }

    private void confineCameraPosition()
    {
        camX = focalPoint.transform.position.x;
        camZ = focalPoint.transform.position.z;

        // Y-Axis
        camHeightCur = Mathf.Clamp(camHeightCur, camHeightMin, camHeightMax);

        // X-Axis
        if (focalPoint.transform.position.x < terrain.transform.position.x)
        {
            Debug.Log("Below X Min");
            camX = terrain.transform.position.x;
            camZ = focalPoint.transform.position.z;
        }
        if (focalPoint.transform.position.x > terrain.transform.position.x + terrain.terrainData.size.x)
        {
            Debug.Log("Above X Max");
            camX = terrain.transform.position.x + terrain.terrainData.size.x;
            camZ = focalPoint.transform.position.z;
        }

        // Z-Axis
        if (focalPoint.transform.position.z < terrain.transform.position.z)
        {
            Debug.Log("Below Z Min");
            camX = focalPoint.transform.position.x;
            camZ = terrain.transform.position.z;
        }
        if (focalPoint.transform.position.z > terrain.transform.position.z + terrain.terrainData.size.z)
        {
            Debug.Log("Above Z Max");
            camX = focalPoint.transform.position.x;
            camZ = terrain.transform.position.z + terrain.terrainData.size.z;
        }

        // Get terrain data
        //camY = getTerrainHeight(camX, camZ);
        camY = camHeightCur;

        // Set camera position
        focalPoint.transform.position = new Vector3(camX, camY, camZ);
    }

    private float getTerrainHeight(float _x, float _y)
    {
        return terrain.terrainData.GetHeight((int)_x, (int)_y);
    }

    private float getTerrainHeightAtCamera()
    {
        return terrain.terrainData.GetHeight((int)cam.transform.position.x, (int)cam.transform.position.z);
    }

}
