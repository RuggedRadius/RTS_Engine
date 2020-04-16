using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Manager References")]
    public UI_Manager uiManager;
    public CameraMovement inputManager;
    public SelectionManager selectionManager;
    public ResourcesManager resourceManager;
    public TeamManager teamManager;

    [Header("Player Settings")]
    public Team playerTeam;
    

    void Start()
    {
        uiManager = GetComponentInChildren<UI_Manager>();
        inputManager = GetComponentInChildren<CameraMovement>();
        selectionManager = GetComponentInChildren<SelectionManager>();
        resourceManager = GetComponentInChildren<ResourcesManager>();
        teamManager = GetComponentInChildren<TeamManager>();

        if (!CheckAllManagers())
        {
            Debug.LogError("Missing Game Manager component(s).");
        }
    }

    private bool CheckAllManagers()
    {
        if (uiManager == null)
            return false;
        if (inputManager == null)
            return false;
        if (selectionManager == null)
            return false;
        if (resourceManager == null)
            return false;
        if (teamManager == null)
            return false;
        
        return true;
    }
}
