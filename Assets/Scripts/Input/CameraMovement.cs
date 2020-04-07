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
    private float camHeight;

    [SerializeField]
    private float camDistance;

    [SerializeField]
    private float camAngle;

    [SerializeField]
    private float keyboardMovementSpeed;

    [SerializeField]
    private float camRotationSpeed;

    [SerializeField]
    private float camZoomSpeed;


    float x;
    float y;
    float z;
    Vector3 movementVector;

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
        keyboardMovementSpeed = Mathf.Clamp(keyboardMovementSpeed, 0.01f, 5f);
        confineCameraPosition();
        positionCamera();
        cam.transform.LookAt(focalPoint.transform);
    }

    private void getMouseScroll()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            camHeight -= Input.mouseScrollDelta.y * camZoomSpeed;
            positionCamera();
            keyboardMovementSpeed = camHeight / 100f;
        }
    }

    private void getKeyboardInputs()
    {
        if (Input.GetKey(KeyCode.W))
        {
            x = cam.transform.forward.x;
            y = 0f;
            z = cam.transform.forward.z;
            movementVector = new Vector3(x, y, z);

            focalPoint.transform.position += movementVector * keyboardMovementSpeed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            focalPoint.transform.position += -cam.transform.right * keyboardMovementSpeed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            x = cam.transform.forward.x;
            y = 0f;
            z = cam.transform.forward.z;
            movementVector = new Vector3(x, y, z);

            focalPoint.transform.position += -movementVector * keyboardMovementSpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            focalPoint.transform.position += cam.transform.right * keyboardMovementSpeed;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            Debug.Log("Turning" + Vector3.up * camRotationSpeed);
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
        Vector3 camPosition = (Vector3.forward * camDistance) + (Vector3.up * (camHeight));

        // Set camera position
        cam.transform.localPosition = camPosition;
    }

    private void confineCameraPosition()
    {
        camX = focalPoint.transform.position.x;
        camZ = focalPoint.transform.position.z;

        // Y-Axis
        camHeight = Mathf.Clamp(camHeight, 1f, 100f);

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
        camY = camHeight;

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
