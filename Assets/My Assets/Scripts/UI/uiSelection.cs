using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Interfaces;

public class uiSelection : MonoBehaviour
{
    #region Properties
    [SerializeField]
    private RectTransform unitSingle;
    [SerializeField]
    private RectTransform unitMultiple;
    [SerializeField]
    private RectTransform structureSingle;
    [SerializeField]
    private RectTransform structureMultiple;
    [SerializeField]
    Button testButton;
    [SerializeField]
    public GameObject testUnitPrefab;

    [SerializeField]
    private uiDisplaySingleUnit singleUnit;


    private List<GameObject> curSelectedObjects = new List<GameObject>();
    #endregion

    #region MonoBehaviour
    private void Start()
    {
        unitSingle.gameObject.SetActive(false);
        unitMultiple.gameObject.SetActive(false);
        structureSingle.gameObject.SetActive(false);
        structureMultiple.gameObject.SetActive(false);
    }
    #endregion

    #region UI Methods
    private void showPanel(RectTransform panel)
    {
        unitSingle.gameObject.SetActive(false);
        unitMultiple.gameObject.SetActive(false);
        structureSingle.gameObject.SetActive(false);
        structureMultiple.gameObject.SetActive(false);

        panel.gameObject.SetActive(true);
    }

    public void updateSelection()
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
        // Single structure selected
        if (SelectionManager.selectedStructures.Count == 1)
        {
            showPanel(structureSingle);
            Structure currentStructure = SelectionManager.selectedStructures[0];
            GameObject structure = new GameObject();
            structure.name = currentStructure.structureName;
            structure.transform.parent = unitSingle.transform;
            curSelectedObjects.Add(structure);

            // Add name of structure to UI
            structureSingle.GetComponentInChildren<Text>().text = currentStructure.structureName;

            // Iterate through all functions of structure
            if (currentStructure is IAttacking)
            {
                // Add attack panel
            }
            //...
        }

        // Mulitple structures selected
        if (SelectionManager.selectedStructures.Count > 1)
        {
            showPanel(structureMultiple);

            foreach (Structure _structure in SelectionManager.selectedStructures)
            {
                // Display to UI
                GameObject structure = new GameObject();
                structure.name = _structure.structureName;
                structure.transform.parent = structureMultiple.transform;
                curSelectedObjects.Add(structure);

                // Display panel with structure sprite and health bar
                //...
                Image img = structure.AddComponent<Image>();
                img.color = Color.blue;
            }
        }
    }

    private void DisplayUnits()
    {
        // Single unit
        if (SelectionManager.selectedUnits.Count == 1)
        {
            // Determine unit
            Unit curUnit = SelectionManager.selectedUnits[0];

            // Update UI panel
            singleUnit.updatePanel(curUnit);

            // Show single unit panel
            showPanel(unitSingle);
        }

        // Multiple units
        if (SelectionManager.selectedUnits.Count > 1)
        {
            showPanel(unitMultiple);

            foreach (Unit unit in SelectionManager.selectedUnits)
            {
                // Display to UI
                GameObject unitImage = new GameObject();
                unitImage.name = unit.unitName;
                unitImage.transform.parent = unitMultiple.transform;

                curSelectedObjects.Add(unitImage);

                Image img = unitImage.AddComponent<Image>();
                img.color = Color.red;
            }
        }
    }

    private void clearCurrentUISelections()
    {
        // Clear list 
        curSelectedObjects.Clear();

        // Destroy all multiple unit selections
        for (int i = 0; i < unitMultiple.childCount; i++)
        {
            Destroy(unitMultiple.GetChild(i).gameObject);
        }

        // Destroy all multiple structure selections
        for (int i = 0; i < structureMultiple.childCount; i++)
        {
            Destroy(structureMultiple.GetChild(i).gameObject);
        }

    }
    #endregion
}
