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
    [SerializeField]
    private SelectableType type;

    public Renderer[] renderers; //Assign all child Mesh Renderers

    public Bounds GetObjectBounds()
    {
        Bounds totalBounds = new Bounds();

        for (int i = 0; i < renderers.Length; i++)
        {
            if (totalBounds.center == Vector3.zero)
            {
                totalBounds = renderers[i].bounds;
            }
            else
            {
                totalBounds.Encapsulate(renderers[i].bounds);
            }
        }

        return totalBounds;
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
                    SelectionManager.currentSelection.Add(this.GetComponent<Unit>());
                }
                else
                {
                    // Single select
                    SelectionManager.currentSelection.Clear();
                    SelectionManager.currentSelection.Add(this.GetComponent<Unit>());
                }                
                break;

            case SelectableType.Building:
                if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                {
                    // Multi-select
                    SelectionManager.currentSelection.Add(this.GetComponent<Structure>());
                }
                else
                {
                    // Single select
                    SelectionManager.currentSelection.Clear();
                    SelectionManager.currentSelection.Add(this.GetComponent<Structure>());
                }
                break;
        }


    }
}