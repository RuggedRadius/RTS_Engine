using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Interfaces;

public class UI_ActionPanel : MonoBehaviour
{
    private UI_Manager uiManager;
    private UI_Utilities uiUtils;

    public List<GameObject> currentActionTiles;

    // Start is called before the first frame update
    void Start()
    {
        uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UI_Manager>();
        uiUtils = this.transform.root.GetComponent<UI_Utilities>();
        currentActionTiles = new List<GameObject>();
    }

    public void DisplayUnitActions(Unit unit)
    {
        ClearActionsPanel();

    }

    public void DisplayStructureActions(Structure structure)
    {
        ClearActionsPanel();

        // Iterate through all functions of structure
        if (structure is IAttacking)
        {
            // Add attack panel
            // Create object
            GameObject _structure = new GameObject();
            _structure.name = structure.structureName;
            _structure.transform.parent = this.transform;

            // Add temp image
            Image img = _structure.AddComponent<Image>();
            img.color = Color.blue;
        }
        if (structure is IUnitProducing)
        {
            foreach (GameObject unitPrefab in structure.producableUnits)
            {
                Unit unit = unitPrefab.GetComponent<Unit>();
                Action newAction = new Action("Create " + unit.unitName, unit.uiTileSprite);
                GameObject newProducableUnitTile = uiUtils.createTile(newAction);
                currentActionTiles.Add(newProducableUnitTile);

                // Button event
                newProducableUnitTile.GetComponent<Button>().onClick.AddListener(delegate() {                    
                    structure.createUnit(unitPrefab.GetComponent<Unit>());
                });
            }
        }
    }

    public void ClearActionsPanel()
    {
        //Debug.Log("Clearing actions");
        for (int i = 0; i < currentActionTiles.Count; i++)
        {
            Destroy(currentActionTiles[i]);
        }
    }
}
