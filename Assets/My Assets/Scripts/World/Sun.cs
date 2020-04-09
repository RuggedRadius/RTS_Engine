using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    [SerializeField]
    private float orbitHeight;
    [SerializeField]
    private float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = new Vector3(0f, orbitHeight, 0f);
    }

    // Update is called once per frame
    Vector3 worldCenter = new Vector3(-2500, 0, -2500);
    void Update()
    {
        this.transform.RotateAround(worldCenter, Vector3.right, Time.deltaTime * rotationSpeed);
        this.transform.LookAt(this.transform.root);
        Debug.DrawRay(this.transform.position, transform.forward);
    }


}
