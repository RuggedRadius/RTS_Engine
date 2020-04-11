using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Interfaces;

public class UI_ActionPanel : MonoBehaviour
{
    [SerializeField]
    private UI_Manager uiManager;

    // Start is called before the first frame update
    void Start()
    {
        uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UI_Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayUnitActions(Unit unit)
    {

    }

    public void DisplayStructureActions(Structure structure)
    {
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
        //...
    }
}
