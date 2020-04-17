using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Interfaces;

public class UI_SelectionPanel : MonoBehaviour
{
    #region Properties
    private UI_Manager uiManager;
    private UI_Utilities uiUtils;
    public List<GameObject> currentSelection;
    public List<GameObject> currentStructureQueue;
    private GameManager gm;
    private SelectionManager sm;
    #endregion

    private void Start()
    {
        uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UI_Manager>();
        uiUtils = uiManager.gameObject.GetComponent<UI_Utilities>();
        currentSelection = new List<GameObject>();
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        sm = gm.GetComponentInChildren<SelectionManager>();
    }
    private void Update()
    {
        if (sm.currentSelection.Count == 1)
        {
            if (sm.currentSelection[0].GetComponent<Structure>() != null)
            {
                PopulateCurrentStructureQueue();
            }
        }
    }

    #region UI Methods
    public void updateUITiles()
    {
        //print("Updating UI Tiles");
        // Clear the UI selections
        ClearSelectionPanel();

        // Prioritise unit selection over buildings
        if (sm.currentSelection.Count > 0)
        {
            if (sm.currentSelection[0].gameObject.GetComponent<Unit>() != null)
            {
                //print("Displaying units");
                DisplayUnits();
            }
            else if (sm.currentSelection[0].gameObject.GetComponent<Structure>() != null)
            {
                //print("Displaying structures");
                DisplayStructures();
            }
            else
            {
                Debug.LogError("No valid current selection");
            }
        }
        else
        {
            Debug.LogError("No selection");
        }
    }

    public void PopulateCurrentStructureQueue()
    {
        ClearCurrentStructureQueue();

        foreach (Unit unit in sm.currentSelection[0].GetComponent<Structure>().productionQueue)
        {
            // Display to UI
            GameObject go = uiManager.gameObject.GetComponent<UI_Utilities>().createTile(unit);
            currentStructureQueue.Add(go);
        }
    }
    public void ClearCurrentStructureQueue()
    {
        // Clear selection panel
        for (int i = 0; i < currentStructureQueue.Count; i++)
        {
            Destroy(currentStructureQueue[i]);
        }
    }

    private void DisplayStructures()
                                            {
        currentSelection.Clear();

        if (sm.currentSelection.Count == 1)
        {
            PopulateCurrentStructureQueue();
        }
        else
        {
            foreach (GameObject potentialStructure in sm.currentSelection)
            {    
                if (potentialStructure.GetComponent<Structure>() != null)
                {
                    // Display to UI
                    Structure myStructure = potentialStructure.GetComponent<Structure>();
                    GameObject go = uiUtils.createTile(myStructure);

                    // Add to currently selected structures
                    currentSelection.Add(go);

                    // Add event handlers to button
                    go.GetComponent<Button>().onClick.AddListener(delegate () {
                        sm.currentSelection.Clear();
                        sm.currentSelection.Clear();
                        sm.currentSelection.Add(potentialStructure.gameObject);
                        updateUITiles();
                    });
                }
            }
        }
    }
    private void DisplayUnits()
    {
        currentSelection.Clear();

        foreach (GameObject potentialUnit in sm.currentSelection)
        {
            if (potentialUnit.GetComponent<Unit>() != null)
            {
                // Determine if single or multiple display
                bool multiple;
                if (sm.currentSelection.Count > 1)
                    multiple = true;
                else
                    multiple = false;

                // Display to UI
                Unit currentUnit = potentialUnit.GetComponent<Unit>();
                GameObject go = uiUtils.createTile(currentUnit);

                // Add event handlers to button
                go.GetComponent<Button>().onClick.AddListener(delegate ()
                {
                    sm.currentSelection.Clear();
                    sm.currentSelection.Add(potentialUnit);
                    updateUITiles();
                });

                // Add to currently selected units
                currentSelection.Add(go);

                // Populate actions UI panel
                if (!multiple)
                {
                    // Display Unit Procedure HERE...

                    // Actions
                    uiManager.panelAction.DisplayUnitActions((Unit)sm.currentSelection[0].GetComponent<Unit>());

                    // Information
                    uiManager.panelInformation.UpdateTextInformation((Unit)potentialUnit.GetComponent<Unit>());
                }
            }
        }        
    }    

    public void ClearSelectionPanel()
    {
        // Clear selection panel
        for (int i = 0; i < currentSelection.Count; i++)
        {
            Destroy(currentSelection[i]);
        }

        ClearCurrentStructureQueue();
    }
    #endregion
}
