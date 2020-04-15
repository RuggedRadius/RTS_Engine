using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitColour : MonoBehaviour
{
    //[SerializeField]
    private Unit unit;

    [SerializeField]
    public MeshRenderer[] colouredRenderers;

    private TeamManager tm;

    void Start()
    {
        tm = GameObject.FindGameObjectWithTag("TeamManager").GetComponent<TeamManager>();
        unit = this.transform.parent.GetComponent<Unit>();

        foreach (MeshRenderer mr in colouredRenderers)
        {
            mr.material = GetTeamColour(unit.team);
        }
    }

    private Material GetTeamColour(Team team)
    {
        switch (team)
        {
            case Team.Team1:
                return tm.teamMaterials[0];

            case Team.Team2:
                return tm.teamMaterials[1];

            case Team.Team3:
                return tm.teamMaterials[2];

            case Team.Team4:
                return tm.teamMaterials[3];

            case Team.Team5:
                return tm.teamMaterials[4];

            case Team.Team6:
                return tm.teamMaterials[5];

            case Team.Team7:
                return tm.teamMaterials[6];

            case Team.Team8:
                return tm.teamMaterials[7];

            default:
                return null;
        }
    }

    private void ColourUnit()
    {

    }
}
