using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_InformationPanel : MonoBehaviour
{
    private UI_Manager uiManager;

    [Header("Components")]
    [SerializeField]
    private Text valueName;
    [SerializeField]
    private Text value1;
    [SerializeField]
    private Text value2;
    [SerializeField]
    private Text value3;

    [Header("Labels")]
    [SerializeField]
    private Text label1;
    [SerializeField]
    private Text label2;
    [SerializeField]
    private Text label3;



    void Start()
    {
        uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UI_Manager>();
    }

    public void ClearInformationPanel()
    {
        valueName.text = string.Empty;
        value1.text = string.Empty;
        value2.text = string.Empty;
        value3.text = string.Empty;

        label1.text = string.Empty;
        label2.text = string.Empty;
        label3.text = string.Empty;
    }

    private void SetPanelForUnit()
    {
        label1.text = "Attack";
        label2.text = "Armour";
        label3.text = "Speed";
    }


    public void UpdateTextInformation(Unit unit)
    {
        SetPanelForUnit();

        valueName.text = unit.unitName;
        value1.text = unit.stats.unitBaseDamage.ToString();
        value2.text = unit.stats.unitBaseArmour.ToString();
        value3.text = unit.stats.unitSpeed.ToString();
    }
    public void UpdateTextInformation(Structure structure)
    {
        valueName.text = structure.structureName;
        value1.text = "Hmm";
        value2.text = "Hmm";
        value3.text = "Hmm";
    }
}
