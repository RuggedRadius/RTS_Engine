using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uiSelection : MonoBehaviour
{
    private List<GameObject> curSelectedObjects = new List<GameObject>();


    GameObject selectionParent;

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
    public GameObject testUnitPrefab;

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
        // Single unit
        if (SelectionManager.selectedUnits.Count == 1)
        {
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

        // Then buildings
        // Single structure selected
        if (SelectionManager.selectedStructures.Count == 1)
        {
            showPanel(structureSingle);

            GameObject structure = new GameObject();
            structure.name = SelectionManager.selectedStructures[0].structureName;
            structure.transform.parent = unitSingle.transform;
            curSelectedObjects.Add(structure);

            // Add name of structure to UI
            structureSingle.GetComponentInChildren<Text>().text = SelectionManager.selectedStructures[0].structureName;

            // Iterate through all functions of structure
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
}
