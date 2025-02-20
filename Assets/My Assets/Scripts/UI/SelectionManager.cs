﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    [Header("Unit Selection Border Sprites")]
    public Texture topLeftBorder;
    public Texture bottomLeftBorder;
    public Texture topRightBorder;
    public Texture bottomRightBorder;

    Texture2D _borderTexture;
    Texture2D borderTexture
    {
        get
        {
            if (_borderTexture == null)
            {
                _borderTexture = new Texture2D(1, 1);
                _borderTexture.SetPixel(0, 0, Color.white);
                _borderTexture.Apply();
            }

            return _borderTexture;
        }
    }

    bool selectionStarted = false;
    Vector3 mousePosition1;
    public LayerMask terrainSearchLayer;
    public LayerMask unitSearchLayer;

    // Cannot be serialised as they are static
    public static List<Selectable> selectables = new List<Selectable>();
    public List<GameObject> currentSelection = new List<GameObject>();

    [Header("Layers")]
    [SerializeField]
    public LayerMask terrainSeekingRayLayer;

    [Header("Prefabs")]
    [SerializeField]
    private GameObject targetLocationPrefab;

    private Camera cam;
    private Vector3 lastTargetLocation;
    private GameObject lastTargetLocationObject;
    private Ray targetRay;

    private UI_Manager uiManager;

    private void Start()
    {
        uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UI_Manager>();
        cam = Camera.main;
    }

    public bool sendingMoveOrder;
    void Update()
    {
        // Begin selection
        if (Input.GetMouseButtonDown(0))
        {

            if (isMouseOverUI())
            {
                // Mouse is over UI
            }
            else
            {
                // Mouse is not over UI

                if (!sendingMoveOrder)
                {
                    selectionStarted = true;
                    mousePosition1 = Input.mousePosition;  
                }
                else
                {
                    // Complete move order

                    // Determine world position based on mouse position
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    Debug.DrawLine(ray.origin, ray.direction * 10000f, Color.red, 1f);
                    if (Physics.Raycast(ray, out RaycastHit hit, 10000f, terrainSearchLayer, QueryTriggerInteraction.Ignore))
                    {
                        print(hit.collider.name);
                        print("Sending unit to " + hit.point);
                        moveSelectedUnitsTo(hit.point);
                        sendingMoveOrder = false;
                    }
                    else
                    {
                        print("Didnt hit terrain with mouse order mouse click");
                    }
                }
            }
        }

        // End selection
        if (Input.GetMouseButtonUp(0))
        {
            selectionStarted = false;
            uiManager.UpdateUI();
        }

        if (selectionStarted)
        {
            // Detect which Objects are inside selection rectangle
            Camera camera = Camera.main;
            currentSelection.Clear();

            for (int i = 0; i < selectables.Count; i++)
            {
                // Get bounds of drag selection
                Bounds viewportBounds = GetViewportBounds(camera, mousePosition1, Input.mousePosition);

                // Get current world position of this selectable
                Vector3 currentSelectableWorldPosition = camera.WorldToViewportPoint(selectables[i].transform.position);

                // If inside...
                if (viewportBounds.Contains(currentSelectableWorldPosition))
                {

                    // Structure
                    if (selectables[i].gameObject.GetComponent<Structure>() != null)
                    {
                        //Debug.Log("Selecting structure");
                        currentSelection.Add(selectables[i].gameObject);
                    }

                    // Units 
                    if (selectables[i].gameObject.GetComponent<Unit>() != null)
                    {

                        // CHANGE THIS ONCE DIPLOMACY IS IMPLEMENTED!!!!!!!!!!!!!!!!!!!!!!!!

                        // IF ENEMY
                        if (selectables[i].gameObject.GetComponent<Unit>().team != GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().playerTeam)
                        {
                            
                            //if (currentSelection.Count == 0)
                            //{
                            //    print("adding enemy to empty selection");

                            //    // Add unit to selection
                            //    RemoveStructuresFromSelections();

                            //    // Ground Melee       
                            //    if (selectables[i].gameObject.GetComponent<Unit_GroundMelee>() != null)
                            //    {
                            //        AddUnitToSelection(selectables[i].gameObject.GetComponent<Unit_GroundMelee>());
                            //    }

                            //    // Ground Ranged      
                            //    if (selectables[i].gameObject.GetComponent<Unit_GroundRanged>() != null)
                            //    {
                            //        AddUnitToSelection(selectables[i].gameObject.GetComponent<Unit_GroundRanged>());
                            //    }

                            //    // Air       
                            //    if (selectables[i].gameObject.GetComponent<Unit_Air>() != null)
                            //    {
                            //        AddUnitToSelection(selectables[i].gameObject.GetComponent<Unit_Air>());
                            //    }

                            //    // Air Transport      
                            //    if (selectables[i].gameObject.GetComponent<Unit_Air>() != null)
                            //    {
                            //        AddUnitToSelection(selectables[i].gameObject.GetComponent<Unit_Air>());
                            //    }
                            //}
                            //else
                            //{
                            //    // If current selection contains enemy units already..
                            //    if (currentSelection[0].GetComponent<Unit>().team != GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().playerTeam)
                            //    {
                            //        print("adding enemy to enemies selection");

                            //        // Add unit to selection
                            //        RemoveStructuresFromSelections();

                            //        // Ground Melee       
                            //        if (selectables[i].gameObject.GetComponent<Unit_GroundMelee>() != null)
                            //        {
                            //            AddUnitToSelection(selectables[i].gameObject.GetComponent<Unit_GroundMelee>());
                            //        }

                            //        // Ground Ranged      
                            //        if (selectables[i].gameObject.GetComponent<Unit_GroundRanged>() != null)
                            //        {
                            //            AddUnitToSelection(selectables[i].gameObject.GetComponent<Unit_GroundRanged>());
                            //        }

                            //        // Air       
                            //        if (selectables[i].gameObject.GetComponent<Unit_Air>() != null)
                            //        {
                            //            AddUnitToSelection(selectables[i].gameObject.GetComponent<Unit_Air>());
                            //        }

                            //        // Air Transport      
                            //        if (selectables[i].gameObject.GetComponent<Unit_Air>() != null)
                            //        {
                            //            AddUnitToSelection(selectables[i].gameObject.GetComponent<Unit_Air>());
                            //        }
                            //    }
                            //    else
                            //    {
                            //        // Current selection already contains friendly units, ignoring enemy unit selection
                            //    }
                            //}
                        }
                        // IF FRIENDLY UNIT
                        else
                        {
                            print("Clicked frinedly unit");

                            // Add unit to selection
                            RemoveStructuresFromSelections();

                            // Attempt to add unit
                            AddUnitToSelection(selectables[i].gameObject.GetComponent<Unit>());
                        }
                    }
                }
            }
        }

        // If right click
        if (Input.GetMouseButtonDown(1))
        {
            // If at least one unit/structure selected
            if (currentSelection.Count > 0)
            {
                // Determine world position based on mouse position
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Debug.DrawLine(ray.origin, ray.direction * 10000f, Color.red, 1f);

                // Look for units
                if (Physics.Raycast(ray, out RaycastHit hitEnemy, 10000f, unitSearchLayer, QueryTriggerInteraction.Ignore))
                {
                    //print("hit on enemy layer");
                    // Hit something
                    if (hitEnemy.collider.GetComponent<Unit>() != null)
                    {
                        // hit a unit
                        if (hitEnemy.collider.GetComponent<Unit>().team != GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().playerTeam)
                        {
                            // Enemy
                            CurrentSelectionAttack(hitEnemy.collider.gameObject);

                        }
                        else
                        {
                            // Friendly Unit
                        }
                    }
                    else
                    {
                        Debug.LogError("Raycast hit unit layer but couldn't find Unit script");
                    }
                }

                // Look for terain
                else if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hitTerrain, 10000, terrainSeekingRayLayer, QueryTriggerInteraction.Ignore))
                {
                    // If hit terrain
                    if (hitTerrain.collider.gameObject.layer == 8)
                    {
                        if (currentSelection[0].GetComponent<Structure>() != null)
                        {
                            // Structure(s) selected
                            currentSelection[0].GetComponent<Structure>().UpdateRallyPoint(hitTerrain.point);
                        }
                        else
                        {
                            // Unit(s) selected

                            // Set location at hit point
                            lastTargetLocation = hitTerrain.point;

                            // Move selected unit(s) to location
                            moveSelectedUnitsTo(lastTargetLocation);

                            // Add fx to location
                            createTargetMarker(lastTargetLocation);
                        }
                    }
                }
            }
        }
    }
    private void AddUnitToSelection(Unit unit)
    {
        // Check current selection count is under maximum
        if (currentSelection.Count < 24)
        {
            // Add unit
            currentSelection.Add(unit.gameObject);
        }
    }

    private void RemoveStructuresFromSelections()
    {

        for (int i = 0; i < currentSelection.Count; i++)
        {
            if (currentSelection[i].GetComponent<Structure>() != null)
            {
                currentSelection.Remove(currentSelection[i]);
            }
        }
    }

    private void CurrentSelectionAttack(GameObject target)
    {
        foreach (dynamic d in currentSelection)
        {
            if (d.GetComponent<Unit>() != null)
            {
                d.GetComponent<Unit>().attack.attack(target);
            }
            else
            {
                Debug.LogError("No unit script found for unit in current selection");
            }
        }
    }

    void OnGUI()
    {
        if (selectionStarted)
        {
            Rect rect = GetScreenRect(mousePosition1, Input.mousePosition);
            DrawScreenRectBorder(rect, 2, Color.cyan);
        }

        // Draw selection edges for current selection
        if (currentSelection.Count > 0)
        {
            Camera camera = Camera.main;
            for (int i = 0; i < currentSelection.Count; i++)
            {
                int index = selectables.IndexOf(currentSelection[i].gameObject.GetComponentInChildren<Selectable>());
                DrawSelectionIndicator(camera, selectables[index].GetObjectBounds());
            }
        }
    }

    private void createTargetMarker(Vector3 position)
    {
        //print("Creating target marker at " + position);
        if (lastTargetLocationObject != null)
        {
            Destroy(lastTargetLocationObject);
        }
        lastTargetLocationObject = Instantiate(targetLocationPrefab, this.transform);
        lastTargetLocation = new Vector3(lastTargetLocation.x, lastTargetLocation.y + 0.25f, lastTargetLocation.z);
        lastTargetLocationObject.transform.position = lastTargetLocation;
        float camHeight = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().GetComponentInChildren<CameraMovement>().camHeightCur;
        lastTargetLocationObject.transform.localScale = Vector3.one * (camHeight / 5f);
    }
    private void moveSelectedUnitsTo(Vector3 _destination)
    {
        foreach (dynamic unit in currentSelection)
        {
            if (unit.GetComponent<Unit>() != null)
            {
                unit.GetComponent<Unit>().movement.move(_destination);
            }
        }
    }
    private bool isMouseOverUI()
    {
        //Set up the new Pointer Event
        PointerEventData m_PointerEventData = new PointerEventData(uiManager.GetComponent<EventSystem>());

        //Set the Pointer Event Position to that of the mouse position
        m_PointerEventData.position = Input.mousePosition;

        //Create a list of Raycast Results
        List<RaycastResult> results = new List<RaycastResult>();

        //Raycast using the Graphics Raycaster and mouse click position
        uiManager.GetComponent<GraphicRaycaster>().Raycast(m_PointerEventData, results);

        //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
        if (results.Count > 0)
        {
            if (results[0].gameObject.layer == 5)
            {
                // Mouse is over UI
                return true;
            }
            else
            {
                // Mouse is not over UI
                return false;
            }
        }
        else
        {
            // Mouse is not over UI
            return false;
        }
    }

    void DrawScreenRectBorder(Rect rect, float thickness, Color color)
    {
        // Top
        DrawBorderRect(new Rect(rect.xMin, rect.yMin, rect.width, thickness), color);
        // Left
        DrawBorderRect(new Rect(rect.xMin, rect.yMin, thickness, rect.height), color);
        // Right
        DrawBorderRect(new Rect(rect.xMax - thickness, rect.yMin, thickness, rect.height), color);
        // Bottom
        DrawBorderRect(new Rect(rect.xMin, rect.yMax - thickness, rect.width, thickness), color);
    }
    void DrawBorderRect(Rect rect, Color color)
    {
        GUI.color = color;
        GUI.DrawTexture(rect, borderTexture);
        GUI.color = Color.white;
    }
    Rect GetScreenRect(Vector3 screenPosition1, Vector3 screenPosition2)
    {
        // Move origin from bottom left to top left
        screenPosition1.y = Screen.height - screenPosition1.y;
        screenPosition2.y = Screen.height - screenPosition2.y;
        // Calculate corners
        var topLeft = Vector3.Min(screenPosition1, screenPosition2);
        var bottomRight = Vector3.Max(screenPosition1, screenPosition2);
        // Create Rect
        return Rect.MinMaxRect(topLeft.x, topLeft.y, bottomRight.x, bottomRight.y);
    }
    Bounds GetViewportBounds(Camera camera, Vector3 screenPosition1, Vector3 screenPosition2)
    {
        Vector3 v1 = camera.ScreenToViewportPoint(screenPosition1);
        Vector3 v2 = camera.ScreenToViewportPoint(screenPosition2);
        Vector3 min = Vector3.Min(v1, v2);
        Vector3 max = Vector3.Max(v1, v2);
        min.z = camera.nearClipPlane;
        max.z = camera.farClipPlane;

        Bounds bounds = new Bounds();
        bounds.SetMinMax(min, max);
        return bounds;
    }
    void DrawSelectionIndicator(Camera camera, Bounds bounds)
    {
        Vector3 boundPoint1 = bounds.min;
        Vector3 boundPoint2 = bounds.max;
        Vector3 boundPoint3 = new Vector3(boundPoint1.x, boundPoint1.y, boundPoint2.z);
        Vector3 boundPoint4 = new Vector3(boundPoint1.x, boundPoint2.y, boundPoint1.z);
        Vector3 boundPoint5 = new Vector3(boundPoint2.x, boundPoint1.y, boundPoint1.z);
        Vector3 boundPoint6 = new Vector3(boundPoint1.x, boundPoint2.y, boundPoint2.z);
        Vector3 boundPoint7 = new Vector3(boundPoint2.x, boundPoint1.y, boundPoint2.z);
        Vector3 boundPoint8 = new Vector3(boundPoint2.x, boundPoint2.y, boundPoint1.z);

        Vector2[] screenPoints = new Vector2[8];
        screenPoints[0] = camera.WorldToScreenPoint(boundPoint1);
        screenPoints[1] = camera.WorldToScreenPoint(boundPoint2);
        screenPoints[2] = camera.WorldToScreenPoint(boundPoint3);
        screenPoints[3] = camera.WorldToScreenPoint(boundPoint4);
        screenPoints[4] = camera.WorldToScreenPoint(boundPoint5);
        screenPoints[5] = camera.WorldToScreenPoint(boundPoint6);
        screenPoints[6] = camera.WorldToScreenPoint(boundPoint7);
        screenPoints[7] = camera.WorldToScreenPoint(boundPoint8);

        Vector2 topLeftPosition = Vector2.zero;
        Vector2 topRightPosition = Vector2.zero;
        Vector2 bottomLeftPosition = Vector2.zero;
        Vector2 bottomRightPosition = Vector2.zero;

        for (int a = 0; a < screenPoints.Length; a++)
        {
            //Top Left
            if (topLeftPosition.x == 0 || topLeftPosition.x > screenPoints[a].x)
            {
                topLeftPosition.x = screenPoints[a].x;
            }
            if (topLeftPosition.y == 0 || topLeftPosition.y > Screen.height - screenPoints[a].y)
            {
                topLeftPosition.y = Screen.height - screenPoints[a].y;
            }
            //Top Right
            if (topRightPosition.x == 0 || topRightPosition.x < screenPoints[a].x)
            {
                topRightPosition.x = screenPoints[a].x;
            }
            if (topRightPosition.y == 0 || topRightPosition.y > Screen.height - screenPoints[a].y)
            {
                topRightPosition.y = Screen.height - screenPoints[a].y;
            }
            //Bottom Left
            if (bottomLeftPosition.x == 0 || bottomLeftPosition.x > screenPoints[a].x)
            {
                bottomLeftPosition.x = screenPoints[a].x;
            }
            if (bottomLeftPosition.y == 0 || bottomLeftPosition.y < Screen.height - screenPoints[a].y)
            {
                bottomLeftPosition.y = Screen.height - screenPoints[a].y;
            }
            //Bottom Right
            if (bottomRightPosition.x == 0 || bottomRightPosition.x < screenPoints[a].x)
            {
                bottomRightPosition.x = screenPoints[a].x;
            }
            if (bottomRightPosition.y == 0 || bottomRightPosition.y < Screen.height - screenPoints[a].y)
            {
                bottomRightPosition.y = Screen.height - screenPoints[a].y;
            }
        }

        GUI.DrawTexture(new Rect(topLeftPosition.x - 16, topLeftPosition.y - 16, 16, 16), topLeftBorder);
        GUI.DrawTexture(new Rect(topRightPosition.x, topRightPosition.y - 16, 16, 16), topRightBorder);
        GUI.DrawTexture(new Rect(bottomLeftPosition.x - 16, bottomLeftPosition.y, 16, 16), bottomLeftBorder);
        GUI.DrawTexture(new Rect(bottomRightPosition.x, bottomRightPosition.y, 16, 16), bottomRightBorder);
    }
}