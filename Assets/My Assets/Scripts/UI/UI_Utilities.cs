using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting;
using UnityEngine;
using UnityEngine.UI;

public static class UI_Utilities
{
    public static GameObject createTile(Unit unit)
    {
        GameObject newTile = new GameObject();
        newTile.name = unit.unitName;

        Image img = newTile.AddComponent<Image>();
        img.sprite = unit.uiTileSprite;
        img.color = Color.red;

        return newTile;
    }
    public static GameObject createTile(Structure structure)
    {
        GameObject newTile = new GameObject();
        Image img = newTile.AddComponent<Image>();

        newTile.name = structure.structureName;
        img.sprite = structure.uiTileSprite;
        img.color = Color.blue;

        return newTile;
    }
    public static GameObject createTile(Action action)
    {
        GameObject newTile = new GameObject();
        Image img = newTile.AddComponent<Image>();

        newTile.name = action.actionName;
        img.sprite = action.uiTileSprite;
        img.color = Color.green;

        return newTile;
    }
}
