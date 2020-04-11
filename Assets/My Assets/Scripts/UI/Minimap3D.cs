using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap3D : MonoBehaviour
{
    [SerializeField]
    private UI_Manager uiManager;

    [SerializeField]
    private Camera cam3DMinimap;

    [SerializeField]
    private Terrain terrain;

    [SerializeField]
    private float orbitSpeed;

    private Vector3 orbitPoint;

    void Start()
    {
        orbitPoint = Vector3.zero;
    }

    void Update()
    {
        cam3DMinimap.transform.RotateAround(orbitPoint, Vector3.up * orbitSpeed, Time.deltaTime);
    }
}
