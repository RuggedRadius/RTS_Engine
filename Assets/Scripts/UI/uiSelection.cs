using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uiSelection : MonoBehaviour
{
    [SerializeField]
    private UnitSelection units;

    void Start()
    {
        
    }

    void Update()
    {
        foreach (Unit unit in units.selectedUnits)
        {
            // Display to UI
        }
    }
}
