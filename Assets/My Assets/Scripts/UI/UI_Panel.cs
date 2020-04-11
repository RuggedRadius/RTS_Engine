using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Panel : MonoBehaviour
{
    private UI_Manager uiManager;

    private void Start()
    {
        uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UI_Manager>();
    }

    private void OnMouseEnter()
    {
        uiManager.mouseOverUI = true;
        print("Mouse over UI element");
    }
    private void OnMouseExit()
    {
        uiManager.mouseOverUI = true;
    }

}
