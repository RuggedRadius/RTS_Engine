using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustScreenDisplayPanels : MonoBehaviour
{
    [SerializeField]
    RectTransform panelPlay;

    [SerializeField]
    RectTransform panelBottom;

    void Start()
    {
        panelBottom.sizeDelta = new Vector2(panelBottom.sizeDelta.x, Screen.height * 0.2f);
        panelPlay.sizeDelta = new Vector2(panelPlay.sizeDelta.x, Screen.height - panelBottom.sizeDelta.y);
    }

    void Update()
    {
        
    }


}
