using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UnitReference
{
    [SerializeField]
    int unitID;
    [SerializeField]
    string unitName;
    [SerializeField]
    float unitSpeed;
    [SerializeField]
    float unitBaseDamage;
    [SerializeField]
    GameObject prefab;
}


public class UnitDictionary : MonoBehaviour
{
    [SerializeField]
    public List<UnitReference> units;
}
