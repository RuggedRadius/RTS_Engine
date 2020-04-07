using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMap : MonoBehaviour
{
    [SerializeField]
    private Camera camMinimap;

    [SerializeField]
    private RawImage uiMinimap;

    [SerializeField]
    private float minimapZoom;

    void Update()
    {
        getKeyboardInputs();
        clampZoom();
        camMinimap.orthographicSize = minimapZoom;
    }

    private void clampZoom()
    {
        minimapZoom = Mathf.Clamp(minimapZoom, 2f, 30f);
    }

    private void getKeyboardInputs()
    {
        if (Input.GetKey(KeyCode.KeypadPlus))
        {
            minimapZoom += 0.1f;
        }
        if (Input.GetKey(KeyCode.KeypadMinus))
        {
            minimapZoom -= 0.1f;
        }

    }
}
