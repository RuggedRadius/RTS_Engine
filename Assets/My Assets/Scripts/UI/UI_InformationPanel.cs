using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_InformationPanel : MonoBehaviour
{
    private UI_Manager uiManager;

    [Header("Components")]
    [SerializeField]
    private Text nameLabel;
    [SerializeField]
    private Text valueAttack;
    [SerializeField]
    private Text valueArmour;
    [SerializeField]
    private Text valueSpeed;
    [SerializeField]
    private Camera portraitCamera;

    void Start()
    {
        uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UI_Manager>();
    }

    public void ClearInformationPanel()
    {
        nameLabel.text = string.Empty;
        valueAttack.text = string.Empty;
        valueArmour.text = string.Empty;
        valueSpeed.text = string.Empty;
    }


    public void UpdateTextInformation(Unit unit)
    {
        nameLabel.text = unit.unitName;
        valueAttack.text = unit.unitBaseDamage.ToString();
        valueArmour.text = unit.unitBaseArmour.ToString();
        valueSpeed.text = unit.unitSpeed.ToString();
    }
    public void UpdateTextInformation(Structure structure)
    {
        nameLabel.text = structure.structureName;
        valueAttack.text = "Hmm";
        valueArmour.text = "Hmm";
        valueSpeed.text = "Hmm";
    }

    public void DisplayInformation(Unit unit)
    {
        // Update info
        UpdateTextInformation(unit);
    }
    public void DisplayInformation(Structure structure)
    {
        // Update info
        UpdateTextInformation(structure);

        // Position camera
        portraitCamera.transform.position = new Vector3(
            structure.gameObject.transform.position.x,
            structure.gameObject.transform.position.z + 1f,
            structure.gameObject.transform.position.x - 5f
            );
        portraitCamera.transform.LookAt(structure.transform);
    }
}
