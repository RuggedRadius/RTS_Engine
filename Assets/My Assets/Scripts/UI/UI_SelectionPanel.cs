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
        if (SelectionManager.selectedStructures.Count == 1)
        {
            PopulateCurrentStructureQueue();


        }
    }
    #endregion

    #region UI Methods
    public void updateUITiles()
    {
        // Clear the UI selections
        ClearSelectionPanel();

        // Prioritise unit selection over buildings
        if (SelectionManager.selectedUnits.Count > 0)
        {
            DisplayUnits();
        }
        else if (SelectionManager.selectedStructures.Count > 0)
        {
            // Then buildings
            DisplayStructures();
        }
    }

    public void PopulateCurrentStructureQueue()
    {
        ClearCurrentStructureQueue();

        foreach (Unit unit in SelectionManager.selectedStructures[0].productionQueue)
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

        if (SelectionManager.selectedStructures.Count == 1)
        {
            PopulateCurrentStructureQueue();
        }
        else
        {
            foreach (Structure _structure in SelectionManager.selectedStructures)
            {    
                // Display to UI
                GameObject go = uiManager.gameObject.GetComponent<UI_Utilities>().createTile(_structure);

                // Add to currently selected structures
                currentSelection.Add(go);
            }
        }


    }
    private void DisplayUnits()
    {
        currentSelection.Clear();

        foreach (Unit unit in SelectionManager.selectedUnits)
        {
            // Determine if single or multiple display
            bool multiple;
            if (SelectionManager.selectedUnits.Count > 1)
                multiple = true;
            else
                multiple = false;

            // Display to UI
            GameObject go = uiManager.gameObject.GetComponent<UI_Utilities>().createTile(unit);

            // Add to currently selected units
            currentSelection.Add(go);

            // Populate actions UI panel
            if (!multiple)
            {
                // Display Unit Procedure HERE...

                // Actions
                uiManager.panelAction.DisplayUnitActions(SelectionManager.selectedUnits[0]);

                // Information
                uiManager.panelInformation.DisplayInformation(unit);
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
