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
    #endregion

    #region MonoBehaviour
    private void Start()
    {
        uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UI_Manager>();
        currentSelection = new List<GameObject>();
    }
    #endregion

    #region UI Methods
    public void updateUITiles()
    {
        // Clear the UI selections
        clearCurrentUISelections();

        // Prioritise unit selection over buildings
        if (SelectionManager.selectedUnits.Count > 0)
            DisplayUnits();

        // Then buildings
        if (SelectionManager.selectedStructures.Count > 0)
            DisplayStructures();
    }

    private void DisplayStructures()
    {
        currentSelection.Clear();

        if (SelectionManager.selectedStructures.Count == 1)
        {
            foreach (Unit unit in SelectionManager.selectedStructures[0].productionQueue)
            {
                // Display to UI
                GameObject go = uiManager.gameObject.GetComponent<UI_Utilities>().createTile(unit);

                // Populate information UI panel
                // ...
            }

            // Populate actions UI panel
            uiManager.panelAction.DisplayStructureActions(SelectionManager.selectedStructures[0]);
        }
        else
        {
            foreach (Structure _structure in SelectionManager.selectedStructures)
            {
                // Determine if single or multiple display
                bool multiple;
                if (SelectionManager.selectedStructures.Count > 1)
                    multiple = true;
                else
                    multiple = false;

                // Display to UI
                GameObject go = uiManager.gameObject.GetComponent<UI_Utilities>().createTile(_structure);

                // Add to currently selected structures
                currentSelection.Add(go);

                // Populate actions UI panel
                if (!multiple)
                {
                    uiManager.panelAction.DisplayStructureActions(SelectionManager.selectedStructures[0]);
                }

                // Populate information UI panel
                // ...
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

            // Populate information UI panel
            // ...
        }
        
    }    

    private void clearCurrentUISelections()
    {
        // Clear selection panel
        for (int i = 0; i < currentSelection.Count; i++)
        {
            Destroy(currentSelection[i]);
        }

        // Clear actions panel
        uiManager.panelAction.clearCurrentUIActions();

        // Clear information panel
        //uiManager.panelInformation.DestroyAllChildren();
    }
    #endregion
}
