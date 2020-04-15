using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotGroups : MonoBehaviour
{
    public GameObject[] hotGroup1;



    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Alpha1))
            {
                print("Hot Group 1 Set.");
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                print("Hot Group 2 Set.");
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                print("Hot Group 3 Set.");
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                print("Hot Group 4 Set.");
            }

        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                print("Hot Group 1");
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                print("Hot Group 2");
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                print("Hot Group 3");
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                print("Hot Group 4");
            }
        }
    }
}
