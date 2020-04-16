using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    private GameManager gm;
    private SelectionManager sm;

    public bool mouseOverUI;

    [Header("Panels")]
    [SerializeField]
    public RectTransform panelTop;
    [SerializeField]
    public RectTransform panelMiddle;
    [SerializeField]
    public RectTransform panelBottom;
    [SerializeField]
    public RectTransform panelMinimap;
    [SerializeField]
    public RectTransform panel3DMinimap;

    [Header("Scripts")]
    [SerializeField]
    public UI_InformationPanel panelInformation;
    [SerializeField]
    public UI_SelectionPanel panelSelection;
    [SerializeField]
    public UI_ActionPanel panelAction;

    public List<GameObject> currentSelection;

    void Start()
    {
        currentSelection = new List<GameObject>();
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        sm = gm.GetComponentInChildren<SelectionManager>();

        sizeMainPanels();
        sizeInteractionSubPanels();
    }

    private void Update()
    {
        GetUpdatedSelection();
    }

    public void sizeMainPanels()
    {
        // Determine panel widths
        float heightTop = 30f;
        float heightBottom = 200f;
        float heightMiddle = Screen.height - heightTop - heightBottom;
        
        // Apply sizes
        panelTop.sizeDelta = new Vector2(Screen.width, heightTop);
        panelMiddle.sizeDelta = new Vector2(Screen.width, heightMiddle) ;
        panelBottom.sizeDelta = new Vector2(Screen.width, heightBottom);
    }
    public void sizeInteractionSubPanels()
    {
        // Get spacings from HorizontalLayoutGroup
        HorizontalLayoutGroup hlg = panelBottom.GetComponent<HorizontalLayoutGroup>();
        float[] paddings = new float[4]; // top, right, bottom, left
        paddings[0] = hlg.padding.top;
        paddings[1] = hlg.padding.right;
        paddings[2] = hlg.padding.bottom;
        paddings[3] = hlg.padding.left;

        // Get RectTransforms
        RectTransform compMinimap = panelBottom.transform.Find("Minimap Panel").GetComponent<RectTransform>();
        RectTransform comp3DMinimap = panelBottom.transform.Find("3D Minimap Panel").GetComponent<RectTransform>();
        RectTransform compInformation = panelBottom.transform.Find("Information Panel").GetComponent<RectTransform>();
        RectTransform compSelection = panelBottom.transform.Find("Selection Panel").GetComponent<RectTransform>();
        RectTransform compAction = panelBottom.transform.Find("Action Panel").GetComponent<RectTransform>();

        // Determine sizes
        float parentWidth = panelBottom.sizeDelta.x;
        float parentHeight = panelBottom.sizeDelta.y;

        // Make minimaps square
        float minimapPanelWidth = panelBottom.rect.height - paddings[0] - paddings[2];
        float minimap3DPanelWidth = panelBottom.rect.height - paddings[0] - paddings[2];

        // Calculate remaining width
        float remainingWidth = parentWidth;
        remainingWidth -= minimapPanelWidth;
        remainingWidth -= minimap3DPanelWidth;
        remainingWidth -= (paddings[0] + paddings[2]);
        remainingWidth -= (4 * hlg.spacing);

        // Calculate panel widths based on remaining width
        float informationPanelWidth = Mathf.Clamp(150f, 150f, parentWidth/2);
        float selectionPanelWidth = Mathf.Clamp(remainingWidth * 0.35f, 200f, parentWidth / 2);
        float actionPanelWidth = Mathf.Clamp(275f, 275f, parentWidth / 2);

        // Apply sizes to panels
        compMinimap.sizeDelta = new Vector2(minimapPanelWidth, compMinimap.sizeDelta.y);
        comp3DMinimap.sizeDelta = new Vector2(minimap3DPanelWidth, comp3DMinimap.sizeDelta.y);
        compInformation.sizeDelta = new Vector2(informationPanelWidth, compInformation.sizeDelta.y);
        compSelection.sizeDelta = new Vector2(selectionPanelWidth, compSelection.sizeDelta.y);
        compAction.sizeDelta = new Vector2(actionPanelWidth, compAction.sizeDelta.y);

        // Adjust minimap grid size
        GridLayoutGroup glg = compMinimap.GetComponent<GridLayoutGroup>();
        float gridSize = minimapPanelWidth;
        gridSize -= glg.padding.top;
        gridSize -= glg.padding.right;
        glg.cellSize = new Vector2(gridSize, gridSize);
    }

    public void ClearUIPanels()
    {
        panelSelection.ClearSelectionPanel();
        panelAction.ClearActionsPanel();
        panelInformation.ClearInformationPanel();
    }

    public void UpdateUI()
    {
        // clear ui here
        ClearUIPanels();

        if (currentSelection.Count < 1)
            return;


        // Selection
        panelSelection.updateUITiles();

        if (currentSelection.Count == 1)
        {
            if (currentSelection[0].GetComponent<Unit>() != null)
            {
                // Action
                panelAction.DisplayUnitActions(currentSelection[0].GetComponent<Unit>());

                // Information
                panelInformation.UpdateTextInformation(currentSelection[0].GetComponent<Unit>());
            }
            else if (currentSelection[0].GetComponent<Structure>() != null)
            {
                // Action
                panelAction.DisplayStructureActions(currentSelection[0].GetComponent<Structure>());

                // Information
                panelInformation.UpdateTextInformation(currentSelection[0].GetComponent<Structure>());
            }
        }
    }

    private void GetUpdatedSelection()
    {
        currentSelection.Clear();

        SelectionManager selManager = GameObject.FindGameObjectWithTag("Selection Manager").GetComponent<SelectionManager>();
        foreach (dynamic d in sm.currentSelection)
        {
            if (d.gameObject != null)
            {
                currentSelection.Add(d.gameObject);
            }
        }
    }
    // Event when screen changes size
}
