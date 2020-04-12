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

    private GameObject currentSelection;

    void Start()
    {
        uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UI_Manager>();
    }

    private void Update()
    {
        //if (currentSelection != null)
        //{
        //    PositionCameraToCurrentSelection();
        //}
        //else
        //{
        //    portraitCamera.enabled = false;
        //}
    }

    private void PositionCameraToCurrentSelection()
    {
        // Check if unit or structure and adjust position
        // ...

        

        //portraitCamera.enabled = true;

        //// Position camera
        //portraitCamera.transform.position = currentSelection.transform.position;
        //portraitCamera.transform.rotation = currentSelection.transform.rotation;
        //portraitCamera.transform.position += (3 * transform.forward);
        //portraitCamera.transform.position += (1.5f * transform.up);
        //portraitCamera.transform.LookAt(currentSelection.transform);
    }

    private void UpdateTextInformation(Unit unit)
    {
        nameLabel.text = unit.unitName;
        valueAttack.text = unit.unitBaseDamage.ToString();
        valueArmour.text = unit.unitBaseArmour.ToString();
        valueSpeed.text = unit.unitSpeed.ToString();
    }
    private void UpdateTextInformation(Structure structure)
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

        portraitCamera = unit.unitCam;

        // Update current selection
        currentSelection = unit.gameObject;
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

        // Update current selection
        currentSelection = structure.gameObject;
    }
}
