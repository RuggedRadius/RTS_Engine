using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotGroups : MonoBehaviour
{
    UI_Manager uiManager;

    public GameObject[] hotGroup1;
    public GameObject[] hotGroup2;
    public GameObject[] hotGroup3;
    public GameObject[] hotGroup4;

    private void Start()
    {
        uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UI_Manager>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Alpha1))
            {
                hotGroup1 = new GameObject[SelectionManager.currentSelection.Count];
                for (int i = 0; i < hotGroup1.Length; i++)
                {
                    hotGroup1[i] = SelectionManager.currentSelection[i].gameObject;
                }
                //print("Hot Group 1 Set.");
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                hotGroup2 = new GameObject[SelectionManager.currentSelection.Count];
                for (int i = 0; i < hotGroup2.Length; i++)
                {
                    hotGroup2[i] = SelectionManager.currentSelection[i].gameObject;
                }
                //print("Hot Group 2 Set.");
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                hotGroup3 = new GameObject[SelectionManager.currentSelection.Count];
                for (int i = 0; i < hotGroup3.Length; i++)
                {
                    hotGroup3[i] = SelectionManager.currentSelection[i].gameObject;
                }
                //print("Hot Group 3 Set.");
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                hotGroup4 = new GameObject[SelectionManager.currentSelection.Count];
                for (int i = 0; i < hotGroup4.Length; i++)
                {
                    hotGroup4[i] = SelectionManager.currentSelection[i].gameObject;
                }
                //print("Hot Group 4 Set.");
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (hotGroup1.Length > 0)
                {
                    SelectionManager.currentSelection.Clear();
                    foreach (GameObject selection in hotGroup1)
                    {
                        SelectionManager.currentSelection.Add(selection);
                    }
                }
                //print("Hot Group 1");
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (hotGroup2.Length > 0)
                {
                    SelectionManager.currentSelection.Clear();
                    foreach (GameObject selection in hotGroup2)
                    {
                        SelectionManager.currentSelection.Add(selection);
                    }
                }
                //print("Hot Group 2");
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                if (hotGroup3.Length > 0)
                {
                    SelectionManager.currentSelection.Clear();
                    foreach (GameObject selection in hotGroup3)
                    {
                        SelectionManager.currentSelection.Add(selection);
                    }
                }
                //print("Hot Group 3");
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                if (hotGroup4.Length > 0)
                {
                    SelectionManager.currentSelection.Clear();
                    foreach (GameObject selection in hotGroup4)
                    {
                        SelectionManager.currentSelection.Add(selection);
                    }
                }
                //print("Hot Group 4");
            }
        }
    }
}
