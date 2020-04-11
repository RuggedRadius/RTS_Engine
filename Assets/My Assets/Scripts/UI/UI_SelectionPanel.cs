using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Interfaces;

public class UI_SelectionPanel : MonoBehaviour
{
    #region Properties
    private UI_Manager uiManager;

    public List<GameObject> currentSelection = new List<GameObject>();
    #endregion

    #region MonoBehaviour
    private void Start()
    {
        uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UI_Manager>();
    }
    #endregion

    #region UI Methods
    public void updateUISelectionDisplay()
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
        Debug.Log("Displaying structure(s)...");
        currentSelection.Clear();

        foreach (Structure _structure in SelectionManager.selectedStructures)
        {
            // Determine if single or multiple display
            bool multiple;
            if (SelectionManager.selectedStructures.Count > 1)
                multiple = true;
            else
                multiple = false;

            // Display to UI
            //GameObject go = createUIObject_Structure(_structure, multiple);
            GameObject go = UI_Utilities.createTile(_structure);



            /// PROBLEM HERE ^^^^^^^^^^^^^^^^^^^^^^^!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            /// PROBLEM HERE ^^^^^^^^^^^^^^^^^^^^^^^!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            /// PROBLEM HERE ^^^^^^^^^^^^^^^^^^^^^^^!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            /// PROBLEM HERE ^^^^^^^^^^^^^^^^^^^^^^^!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            /// PROBLEM HERE ^^^^^^^^^^^^^^^^^^^^^^^!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            /// PROBLEM HERE ^^^^^^^^^^^^^^^^^^^^^^^!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

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
            //GameObject go = createUIObject_Unit(unit, multiple);
            GameObject go = UI_Utilities.createTile(unit);

            // Add to currently selected units
            currentSelection.Add(go);

            // Populate actions UI panel
            if (!multiple)
            {
                uiManager.panelAction.DisplayUnitActions(SelectionManager.selectedUnits[0]);
            }

            // Populate information UI panel
            // ...
        }
        
    }
    private GameObject createUIObject_Structure(Structure structure, bool multiple = false)
    {
        // Create object
        GameObject _structure = new GameObject();
        _structure.name = structure.structureName;
        _structure.transform.parent = this.transform;

        // Add temp image
        Image img = _structure.AddComponent<Image>();
        img.color = Color.blue;

        return _structure;
    }

    private GameObject createUIObject_Unit(Unit unit, bool multiple = false)
    {
        // Create object
        GameObject _unit = new GameObject();
        _unit.name = unit.unitName;
        _unit.transform.parent = this.transform;

        // Add temp image
        Image img = _unit.AddComponent<Image>();
        img.color = Color.red;

        return _unit;
    }

    

    private void clearCurrentUISelections()
    {
        for (int i = 0; i < currentSelection.Count; i++)
        {
            Destroy(currentSelection[i]);
        }
    }
    #endregion
}
