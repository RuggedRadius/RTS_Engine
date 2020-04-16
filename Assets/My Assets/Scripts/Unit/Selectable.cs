using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using UnityEngine;

[Serializable]
public enum SelectableType
{
    Unit,
    Building
}

public class Selectable : MonoBehaviour
{
    private GameManager gm;
    private SelectionManager sm;
    private TeamManager tm;

    [SerializeField]
    private SelectableType type;

    public Renderer[] childRenderers; //Assign all child Mesh Renderers
    public MeshRenderer[] colouredRenderers;

    public Bounds GetObjectBounds()
    {
        Bounds totalBounds = new Bounds();

        for (int i = 0; i < childRenderers.Length; i++)
        {
            if (totalBounds.center == Vector3.zero)
            {
                totalBounds = childRenderers[i].bounds;
            }
            else
            {
                totalBounds.Encapsulate(childRenderers[i].bounds);
            }
        }

        return totalBounds;
    }

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        sm = gm.GetComponentInChildren<SelectionManager>();
        tm = gm.GetComponentInChildren<TeamManager>();

        ColourUnit();

        if (childRenderers.Length == 0)
            Debug.LogWarning("No child renderers set for unit: " + this.gameObject.name);
        if (colouredRenderers.Length == 0)
            Debug.LogWarning("No team coloured renderers set for unit: " + this.gameObject.name);
    }

    void OnEnable()
    {
        //Add this Object to global list
        if (!SelectionManager.selectables.Contains(this))
        {
            SelectionManager.selectables.Add(this);
        }
    }

    void OnDisable()
    {
        //Remove this Object from global list
        if (SelectionManager.selectables.Contains(this))
        {
            SelectionManager.selectables.Remove(this);
        }
    }

    private void OnMouseDown()
    {
        switch (type)
        {
            case SelectableType.Unit:
                if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                {
                    // Multi-select
                    sm.currentSelection.Add(this.gameObject);
                }
                else
                {
                    // Single select
                    sm.currentSelection.Clear();
                    sm.currentSelection.Add(this.gameObject);
                }                
                break;

            case SelectableType.Building:
                if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                {
                    // Multi-select
                    sm.currentSelection.Add(this.gameObject);
                }
                else
                {
                    // Single select
                    sm.currentSelection.Clear();
                    sm.currentSelection.Add(this.gameObject);
                }
                break;
        }


    }

    private void ColourUnit()
    {
        foreach (MeshRenderer mr in colouredRenderers)
        {
            mr.material = GetTeamColour(this.GetComponent<Unit>().team);
        }
    }
    private Material GetTeamColour(Team team)
    {
        switch (team)
        {
            case Team.Team1:
                return tm.teamMaterials[0];

            case Team.Team2:
                return tm.teamMaterials[1];

            case Team.Team3:
                return tm.teamMaterials[2];

            case Team.Team4:
                return tm.teamMaterials[3];

            case Team.Team5:
                return tm.teamMaterials[4];

            case Team.Team6:
                return tm.teamMaterials[5];

            case Team.Team7:
                return tm.teamMaterials[6];

            case Team.Team8:
                return tm.teamMaterials[7];

            default:
                return null;
        }
    }
}