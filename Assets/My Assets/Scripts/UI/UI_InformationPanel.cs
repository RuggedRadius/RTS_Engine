using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_InformationPanel : MonoBehaviour
{
    [SerializeField]
    private UI_Manager uiManager;

    void Start()
    {
        uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UI_Manager>();
    }
}
