using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public UI_Manager uiManager;

    public Team playerTeam;

    

    void Start()
    {
        uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UI_Manager>();
    }

    void Update()
    {
        
    }
}
