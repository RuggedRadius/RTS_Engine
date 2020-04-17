using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotGroups : MonoBehaviour
{
    private GameManager gm;
    private SelectionManager sm;

    UI_Manager uiManager;

    public GameObject[] hotGroup1;
    public GameObject[] hotGroup2;
    public GameObject[] hotGroup3;
    public GameObject[] hotGroup4;

    private void Start()
    {
        uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UI_Manager>();
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        sm = gm.GetComponentInChildren<SelectionManager>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Alpha1))
            {
                hotGroup1 = new GameObject[sm.currentSelection.Count];
                for (int i = 0; i < hotGroup1.Length; i++)
                {
                    hotGroup1[i] = sm.currentSelection[i].gameObject;
                    
                }
                //print("Hot Group 1 Set.");
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                hotGroup2 = new GameObject[sm.currentSelection.Count];
                for (int i = 0; i < hotGroup2.Length; i++)
                {
                    hotGroup2[i] = sm.currentSelection[i].gameObject;
                }
                //print("Hot Group 2 Set.");
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                hotGroup3 = new GameObject[sm.currentSelection.Count];
                for (int i = 0; i < hotGroup3.Length; i++)
                {
                    hotGroup3[i] = sm.currentSelection[i].gameObject;
                }
                //print("Hot Group 3 Set.");
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                hotGroup4 = new GameObject[sm.currentSelection.Count];
                for (int i = 0; i < hotGroup4.Length; i++)
                {
                    hotGroup4[i] = sm.currentSelection[i].gameObject;
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
                    sm.currentSelection.Clear();
                    foreach (GameObject selection in hotGroup1)
                    {
                        sm.currentSelection.Add(selection);
                    }

                    uiManager.GetUpdatedSelection();
                    uiManager.UpdateUI();
                }
                //print("Hot Group 1");
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (hotGroup2.Length > 0)
                {
                    sm.currentSelection.Clear();
                    foreach (GameObject selection in hotGroup2)
                    {
                        sm.currentSelection.Add(selection);
                    }

                    uiManager.GetUpdatedSelection();
                    uiManager.UpdateUI();
                }
                //print("Hot Group 2");
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                if (hotGroup3.Length > 0)
                {
                    sm.currentSelection.Clear();
                    foreach (GameObject selection in hotGroup3)
                    {
                        sm.currentSelection.Add(selection);
                    }

                    uiManager.GetUpdatedSelection();
                    uiManager.UpdateUI();
                }
                //print("Hot Group 3");
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                if (hotGroup4.Length > 0)
                {
                    sm.currentSelection.Clear();
                    foreach (GameObject selection in hotGroup4)
                    {
                        sm.currentSelection.Add(selection);
                    }

                    uiManager.GetUpdatedSelection();
                    uiManager.UpdateUI();
                }
                //print("Hot Group 4");
            }
        }
    }
}
