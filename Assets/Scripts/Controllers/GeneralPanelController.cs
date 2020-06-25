using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GeneralPanelController : MonoBehaviour
{

    [SerializeField]
    private Canvas panelCanvas;

    public void onBackButtonClicked()
    {
        disableCanvas();
    }

    public void disableCanvas()
    {
        panelCanvas.enabled = !enabled;
    }

    public void enableCanvas()
    {
        panelCanvas.enabled = enabled;
    }
}

