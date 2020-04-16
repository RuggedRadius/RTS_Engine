using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Statistics : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    [Range(5f, 20f)]
    public float unitSpeed;
    [SerializeField]
    public float unitAcceleration;
    [SerializeField]
    public float unitRadius;
    [SerializeField]
    public float unitHeight;
    [SerializeField]
    public float unitStoppingDistance;

    [Header("Damage")]
    [SerializeField]
    public float unitBaseDamage;
    [SerializeField]
    public float unitBaseArmour;
}
