using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action
{
    public string actionName;
    public Sprite uiTileSprite;

    public Action(string _name, Sprite _sprite)
    {
        actionName = _name;
        uiTileSprite = _sprite;
    }
}
