using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private RectTransform panelPlay;
    [SerializeField]
    private RectTransform panelUserInteraction;

    [SerializeField]
    [Range(0.1f, 0.25f)]
    private float userInteractionSize;
    private float playSize;

    void Start()
    {
        sizePanels();
    }

    void Update()
    {
        
    }

    private void sizePanels()
    {
        playSize = 1 - userInteractionSize;

        panelPlay.sizeDelta = new Vector2(Screen.width, Screen.height * playSize);
        panelUserInteraction.sizeDelta = new Vector2(Screen.width, Screen.height * userInteractionSize);
    }
}
