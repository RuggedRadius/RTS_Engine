using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamManager : MonoBehaviour
{
    [SerializeField]
    public Color[] teamColours;
    public Material[] teamMaterials;

    private void Awake()
    {
        for (int i = 0; i < teamMaterials.Length; i++)
        {
            teamMaterials[i].color = teamColours[i];
        }
    }
}
