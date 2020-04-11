using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_InformationPanel : MonoBehaviour
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
}
