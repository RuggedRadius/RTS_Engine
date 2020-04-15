using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
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

        switch (unit.unitType)
        {
            case UnitType.GroundRanged:
                if (unit.GetComponent<Unit_GroundRanged>() is IAttacking)
                {
                    // Create tile
                    Action newAction = new Action(
                        "Attack",
                        uiManager.GetComponent<UI_TileManager>().UI_Action_Attack
                        );
                    GameObject newProducableUnitTile = uiUtils.createTile(newAction);
                    currentActionTiles.Add(newProducableUnitTile);

                    // Button event
                    newProducableUnitTile.GetComponent<Button>().onClick.AddListener(delegate () {
                        // Action
                        unit.GetComponent<Unit_GroundRanged>().attack(unit.transform.position);
                    });
                }
                if (unit is IMovable)
                {
                    // MOVE:
                    // Create tile
                    Action newAction = new Action(
                        "Move",
                        uiManager.GetComponent<UI_TileManager>().UI_Action_Move
                        );
                    GameObject newProducableUnitTile = uiUtils.createTile(newAction);
                    currentActionTiles.Add(newProducableUnitTile);

                    // Button event
                    newProducableUnitTile.GetComponent<Button>().onClick.AddListener(delegate ()
                    {
                        // Move Player to next valid click or cancel
                        // ...
                    });

                    // STOP:
                    // Create tile
                    newAction = new Action(
                        "Stop",
                        uiManager.GetComponent<UI_TileManager>().UI_Action_Stop
                        );
                    newProducableUnitTile = uiUtils.createTile(newAction);
                    currentActionTiles.Add(newProducableUnitTile);

                    // Button event
                    newProducableUnitTile.GetComponent<Button>().onClick.AddListener(delegate ()
                    {
                        // Action
                        unit.stopMoving();
                    });
                }
                if (unit is IMovable && unit is IAttacking)
                {
                    // Create tile
                    Action newAction = new Action(
                        "Attack Move",
                        uiManager.GetComponent<UI_TileManager>().UI_Action_AttackMove
                        );
                    GameObject newProducableUnitTile = uiUtils.createTile(newAction);
                    currentActionTiles.Add(newProducableUnitTile);

                    // Button event
                    newProducableUnitTile.GetComponent<Button>().onClick.AddListener(delegate ()
                    {
                        // Action
                        // ...
                    });
                }
                break;

            case UnitType.GroundMelee:
                if (unit is IAttacking)
                {
                    // Create tile
                    Action newAction = new Action(
                        "Attack",
                        uiManager.GetComponent<UI_TileManager>().UI_Action_Attack
                        );
                    GameObject newProducableUnitTile = uiUtils.createTile(newAction);
                    currentActionTiles.Add(newProducableUnitTile);

                    // Button event
                    newProducableUnitTile.GetComponent<Button>().onClick.AddListener(delegate () {
                        // Action
                        switch (unit.unitType)
                        {
                            case UnitType.GroundRanged:
                                unit.GetComponent<Unit_GroundRanged>().attack(unit.transform.position);
                                break;

                            case UnitType.GroundMelee:
                                unit.GetComponent<Unit_GroundMelee>().attack(unit.transform.position);
                                break;

                            case UnitType.Air:
                                unit.GetComponent<Unit_Air>().attack(unit.transform.position);
                                break;

                            case UnitType.AirTransport:
                                unit.GetComponent<Unit_Air>().attack(unit.transform.position);
                                break;

                        }
                    });
                }
                if (unit is IMovable)
                {
                    // MOVE:
                    // Create tile
                    Action newAction = new Action(
                        "Move",
                        uiManager.GetComponent<UI_TileManager>().UI_Action_Move
                        );
                    GameObject newProducableUnitTile = uiUtils.createTile(newAction);
                    currentActionTiles.Add(newProducableUnitTile);

                    // Button event
                    newProducableUnitTile.GetComponent<Button>().onClick.AddListener(delegate ()
                    {
                        // Move Player to next valid click or cancel
                        // ...
                    });

                    // STOP:
                    // Create tile
                    newAction = new Action(
                        "Stop",
                        uiManager.GetComponent<UI_TileManager>().UI_Action_Stop
                        );
                    newProducableUnitTile = uiUtils.createTile(newAction);
                    currentActionTiles.Add(newProducableUnitTile);

                    // Button event
                    newProducableUnitTile.GetComponent<Button>().onClick.AddListener(delegate ()
                    {
                        // Action
                        unit.stopMoving();
                    });
                }
                if (unit is IMovable && unit is IAttacking)
                {
                    // Create tile
                    Action newAction = new Action(
                        "Attack Move",
                        uiManager.GetComponent<UI_TileManager>().UI_Action_AttackMove
                        );
                    GameObject newProducableUnitTile = uiUtils.createTile(newAction);
                    currentActionTiles.Add(newProducableUnitTile);

                    // Button event
                    newProducableUnitTile.GetComponent<Button>().onClick.AddListener(delegate ()
                    {
                        // Action
                        // ...
                    });
                }
                break;

            case UnitType.Air:
                if (unit is IAttacking)
                {
                    // Create tile
                    Action newAction = new Action(
                        "Attack",
                        uiManager.GetComponent<UI_TileManager>().UI_Action_Attack
                        );
                    GameObject newProducableUnitTile = uiUtils.createTile(newAction);
                    currentActionTiles.Add(newProducableUnitTile);

                    // Button event
                    newProducableUnitTile.GetComponent<Button>().onClick.AddListener(delegate () {
                        // Action
                        switch (unit.unitType)
                        {
                            case UnitType.GroundRanged:
                                unit.GetComponent<Unit_GroundRanged>().attack(unit.transform.position);
                                break;

                            case UnitType.GroundMelee:
                                unit.GetComponent<Unit_GroundMelee>().attack(unit.transform.position);
                                break;

                            case UnitType.Air:
                                unit.GetComponent<Unit_Air>().attack(unit.transform.position);
                                break;

                            case UnitType.AirTransport:
                                unit.GetComponent<Unit_Air>().attack(unit.transform.position);
                                break;

                        }
                    });
                }
                if (unit is IMovable)
                {
                    // MOVE:
                    // Create tile
                    Action newAction = new Action(
                        "Move",
                        uiManager.GetComponent<UI_TileManager>().UI_Action_Move
                        );
                    GameObject newProducableUnitTile = uiUtils.createTile(newAction);
                    currentActionTiles.Add(newProducableUnitTile);

                    // Button event
                    newProducableUnitTile.GetComponent<Button>().onClick.AddListener(delegate ()
                    {
                        // Move Player to next valid click or cancel
                        // ...
                    });

                    // STOP:
                    // Create tile
                    newAction = new Action(
                        "Stop",
                        uiManager.GetComponent<UI_TileManager>().UI_Action_Stop
                        );
                    newProducableUnitTile = uiUtils.createTile(newAction);
                    currentActionTiles.Add(newProducableUnitTile);

                    // Button event
                    newProducableUnitTile.GetComponent<Button>().onClick.AddListener(delegate ()
                    {
                        // Action
                        unit.stopMoving();
                    });
                }
                if (unit is IMovable && unit is IAttacking)
                {
                    // Create tile
                    Action newAction = new Action(
                        "Attack Move",
                        uiManager.GetComponent<UI_TileManager>().UI_Action_AttackMove
                        );
                    GameObject newProducableUnitTile = uiUtils.createTile(newAction);
                    currentActionTiles.Add(newProducableUnitTile);

                    // Button event
                    newProducableUnitTile.GetComponent<Button>().onClick.AddListener(delegate ()
                    {
                        // Action
                        // ...
                    });
                }
                break;

            case UnitType.AirTransport:
                if (unit is IAttacking)
                {
                    // Create tile
                    Action newAction = new Action(
                        "Attack",
                        uiManager.GetComponent<UI_TileManager>().UI_Action_Attack
                        );
                    GameObject newProducableUnitTile = uiUtils.createTile(newAction);
                    currentActionTiles.Add(newProducableUnitTile);

                    // Button event
                    newProducableUnitTile.GetComponent<Button>().onClick.AddListener(delegate () {
                        // Action
                        switch (unit.unitType)
                        {
                            case UnitType.GroundRanged:
                                unit.GetComponent<Unit_GroundRanged>().attack(unit.transform.position);
                                break;

                            case UnitType.GroundMelee:
                                unit.GetComponent<Unit_GroundMelee>().attack(unit.transform.position);
                                break;

                            case UnitType.Air:
                                unit.GetComponent<Unit_Air>().attack(unit.transform.position);
                                break;

                            case UnitType.AirTransport:
                                unit.GetComponent<Unit_Air>().attack(unit.transform.position);
                                break;

                        }
                    });
                }
                if (unit is IMovable)
                {
                    // MOVE:
                    // Create tile
                    Action newAction = new Action(
                        "Move",
                        uiManager.GetComponent<UI_TileManager>().UI_Action_Move
                        );
                    GameObject newProducableUnitTile = uiUtils.createTile(newAction);
                    currentActionTiles.Add(newProducableUnitTile);

                    // Button event
                    newProducableUnitTile.GetComponent<Button>().onClick.AddListener(delegate ()
                    {
                        // Move Player to next valid click or cancel
                        // ...
                    });

                    // STOP:
                    // Create tile
                    newAction = new Action(
                        "Stop",
                        uiManager.GetComponent<UI_TileManager>().UI_Action_Stop
                        );
                    newProducableUnitTile = uiUtils.createTile(newAction);
                    currentActionTiles.Add(newProducableUnitTile);

                    // Button event
                    newProducableUnitTile.GetComponent<Button>().onClick.AddListener(delegate ()
                    {
                        // Action
                        unit.stopMoving();
                    });
                }
                if (unit is IMovable && unit is IAttacking)
                {
                    // Create tile
                    Action newAction = new Action(
                        "Attack Move",
                        uiManager.GetComponent<UI_TileManager>().UI_Action_AttackMove
                        );
                    GameObject newProducableUnitTile = uiUtils.createTile(newAction);
                    currentActionTiles.Add(newProducableUnitTile);

                    // Button event
                    newProducableUnitTile.GetComponent<Button>().onClick.AddListener(delegate ()
                    {
                        // Action
                        // ...
                    });
                }
                break;

        }

        //if (unit is IAttacking)
        //{
        //    // Create tile
        //    Action newAction = new Action(
        //        "Attack",
        //        uiManager.GetComponent<UI_TileManager>().UI_Action_Attack
        //        );
        //    GameObject newProducableUnitTile = uiUtils.createTile(newAction);
        //    currentActionTiles.Add(newProducableUnitTile);

        //    // Button event
        //    newProducableUnitTile.GetComponent<Button>().onClick.AddListener(delegate () {
        //        // Action
        //        switch (unit.unitType)
        //        {
        //            case UnitType.GroundRanged:
        //                unit.GetComponent<Unit_GroundRanged>().attack(unit.transform.position);
        //                break;

        //            case UnitType.GroundMelee:
        //                unit.GetComponent<Unit_GroundMelee>().attack(unit.transform.position);
        //                break;

        //            case UnitType.Air:
        //                unit.GetComponent<Unit_Air>().attack(unit.transform.position);
        //                break;

        //            case UnitType.AirTransport:
        //                unit.GetComponent<Unit_Air>().attack(unit.transform.position);
        //                break;

        //        }
        //    });
        //}



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
                // Create tile
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
