using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap3D : MonoBehaviour
{
    [SerializeField]
    private Camera cam3DMinimap;

    [SerializeField]
    private Terrain terrain;

    [SerializeField]
    private float orbitSpeed;

    private Vector3 mapCenter;

    void Start()
    {
        mapCenter = new Vector3(terrain.terrainData.size.x / 2f, 0f, terrain.terrainData.size.z / 2f);
    }

    void Update()
    {
        cam3DMinimap.transform.RotateAround(mapCenter, Vector3.up * orbitSpeed, Time.deltaTime);
    }
}
