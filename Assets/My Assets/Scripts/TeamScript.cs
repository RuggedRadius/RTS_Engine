using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Team
{
    Team1,
    Team2,
    Team3,
    Team4,
    Team5,
    Team6,
    Team7,
    Team8,
}

public class TeamScript : MonoBehaviour
{
    [SerializeField]
    public Team team;

    [HideInInspector]
    public Color teamColour;


    // Start is called before the first frame update
    void Awake()
    {
        AssignTeamColour(team);
    }

    private void AssignTeamColour(Team team)
    {
        switch (team)
        {
            case Team.Team1:
                teamColour = Color.red;
                break;

            case Team.Team2:
                teamColour = Color.blue;
                break;

            case Team.Team3:
                teamColour = Color.green;
                break;

            case Team.Team4:
                teamColour = Color.yellow;
                break;

            case Team.Team5:
                teamColour = Color.magenta;
                break;

            case Team.Team6:
                teamColour = Color.cyan;
                break;

            case Team.Team7:
                teamColour = Color.gray;
                break;

            case Team.Team8:
                teamColour = Color.white;
                break;

            default:
                break;
        }
    }

    //private void ColourExistingUnits()
    //{
    //    if (teamColour != null)
    //    {
    //        // Get all units
    //        GameObject unitsParent = this.transform.Find("Units").gameObject;
    //        GameObject[] exisitingUnits = new GameObject[unitsParent.transform.childCount];
    //        for (int i = 0; i < unitsParent.transform.childCount; i++)
    //        {
    //            exisitingUnits[i] = unitsParent.transform.GetChild(i).gameObject;
    //        }

    //        // Assign colour to unit
    //    }
    //}

    void Update()
    {
        
    }
}
