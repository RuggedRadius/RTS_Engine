using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Interfaces;

public class uiDisplaySingleUnit : MonoBehaviour
{
    [Header("Single Unit Panel Components")]
    [SerializeField]
    private Text unitName;
    [SerializeField]
    private GridLayoutGroup grid;

    [Header("UI Tiles")]
    [SerializeField]
    private Sprite imgAttack;
    [SerializeField]
    private Sprite imgStop;
    

    public void updatePanel(Unit unit)
    {             
        // Update panel information
        unitName.text = unit.unitName;
        if (unit is IMovable)
        {
            addAction(imgAttack);
            addAction(imgStop);
        }
    }

    private void addAction(Sprite icon)
    {
        GameObject newAction = new GameObject();
        newAction.transform.parent = this.transform.Find("Actions Grid");

        Image newImage = newAction.AddComponent<Image>();
        newImage.sprite = icon;
        
    }

}
