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

    [SerializeField]
    private GameObject uiButtonPrefab;

    [Header("UI Tiles")]
    [SerializeField]
    private Sprite imgAttack;
    [SerializeField]
    private Sprite imgStop;

    [SerializeField]
    private Unit currentUnit;
    

    public void updatePanel(Unit unit)
    {
        currentUnit = unit;

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
        // Create button prefab
        GameObject newAction = Instantiate(uiButtonPrefab);
        newAction.transform.parent = this.transform.Find("Actions Grid");

        // Set Sprite
        Button btn = newAction.GetComponent<Button>();
        Image btnImage = newAction.GetComponent<Image>();
        btnImage.sprite = icon;

        // Add OnClick Events
        //...
        btn.onClick.AddListener(btnTest_Click);
    }

    public Structure testStructure;
    void btnTest_Click()
    {
        testStructure.createUnit(currentUnit);
    }

    public void clearActionsGrid()
    {
        Transform parent = this.transform.Find("Actions Grid");
        for (int i = 0; i < parent.childCount; i++)
        {
            Destroy(parent.GetChild(i));
        }
    }

}
