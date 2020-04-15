using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Interfaces;

public class UI_SelectionPanel : MonoBehaviour
{
    #region Properties
    private UI_Manager uiManager;
    public List<GameObject> currentSelection;
    public List<GameObject> currentStructureQueue;
    #endregion

    #region MonoBehaviour
    private void Start()
    {
        uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UI_Manager>();
        currentSelection = new List<GameObject>();
    }
    private void Update()
    {
        if (SelectionManager.currentSelection.Count == 1)
        {
            if (SelectionManager.currentSelection[0].GetComponent<Structure>() != null)
            {
                PopulateCurrentStructureQueue();
            }
        }
    }
    #endregion

    #region UI Methods
    public void updateUITiles()
    {
        //print("Updating UI Tiles");
        // Clear the UI selections
        ClearSelectionPanel();

        // Prioritise unit selection over buildings
        if (SelectionManager.currentSelection.Count > 0)
        {
            if (SelectionManager.currentSelection[0].gameObject.GetComponent<Unit>() != null)
            {
                //print("Displaying units");
                DisplayUnits();
            }
            else if (SelectionManager.currentSelection[0].gameObject.GetComponent<Structure>() != null)
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

        foreach (Unit unit in SelectionManager.currentSelection[0].productionQueue)
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

        if (SelectionManager.currentSelection.Count == 1)
        {
            PopulateCurrentStructureQueue();
        }
        else
        {
            foreach (dynamic _structure in SelectionManager.currentSelection)
            {    
                if (_structure.GetComponent<Structure>() != null)
                {
                    // Display to UI
                    GameObject go = uiManager.gameObject.GetComponent<UI_Utilities>().createTile(_structure);

                    // Add to currently selected structures
                    currentSelection.Add(go);

                    // Add event handlers to button
                    go.GetComponent<Button>().onClick.AddListener(delegate () {
                        SelectionManager.currentSelection.Clear();
                        SelectionManager.currentSelection.Clear();
                        SelectionManager.currentSelection.Add((Structure)_structure);
                        updateUITiles();
                    });
                }
            }
        }
    }
    private void DisplayUnits()
    {
        currentSelection.Clear();

        foreach (dynamic unit in SelectionManager.currentSelection)
        {
            if (unit.GetComponent<Unit>() != null)
            {
                // Determine if single or multiple display
                bool multiple;
                if (SelectionManager.currentSelection.Count > 1)
                    multiple = true;
                else
                    multiple = false;

                // Display to UI
                GameObject go = uiManager.gameObject.GetComponent<UI_Utilities>().createTile((Unit)unit.GetComponent<Unit>());

                // Add event handlers to button
                go.GetComponent<Button>().onClick.AddListener(delegate ()
                {
                    SelectionManager.currentSelection.Clear();
                    SelectionManager.currentSelection.Add(unit);
                    updateUITiles();
                });

                // Add to currently selected units
                currentSelection.Add(go);

                // Populate actions UI panel
                if (!multiple)
                {
                    // Display Unit Procedure HERE...

                    // Actions
                    uiManager.panelAction.DisplayUnitActions((Unit)SelectionManager.currentSelection[0].GetComponent<Unit>());

                    // Information
                    uiManager.panelInformation.UpdateTextInformation((Unit)unit.GetComponent<Unit>());
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
