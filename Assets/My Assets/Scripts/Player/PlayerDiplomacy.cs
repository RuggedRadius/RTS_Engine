using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public enum DiplomacyState
{
    Friendly,
    Neutral,
    Enemy
}

[Serializable]
public class Diplomacy
{
    [SerializeField] public Team team;
    [SerializeField] public DiplomacyState state;
}

public class PlayerDiplomacy : MonoBehaviour
{
    public List<Diplomacy> diplomacies;
}
