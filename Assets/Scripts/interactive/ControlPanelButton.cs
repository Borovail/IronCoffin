using System;
using interactive;
using UnityEngine;

public class ControlPanelButton : MonoBehaviour, Iinteractable
{
    public enum ButtonType { Left, Right, Forward }
    public ButtonType buttonType;
    [SerializeField]
    private ControlPanel controlPanel;
    
    public void OnInteract()
    {
        if (!controlPanel) return;
        switch (buttonType)
        {
            case ButtonType.Left:
                if (controlPanel.rotation <= 0) return;
                controlPanel.StartRotate();
                break;
            case ButtonType.Right:
                if (controlPanel.rotation <= 0) return;
                controlPanel.StartRotate();
                break;
            case ButtonType.Forward:
                if (controlPanel.length <= 0) return;
                controlPanel.StartMoveForward();
                break;
        }
    }

    public void OnRelease()
    {
        if (!controlPanel) return;
        switch (buttonType)
        {
            case ButtonType.Left:
                if (controlPanel.isRotating == false) return;
                controlPanel.StopRotate();
                break;
            case ButtonType.Right:
                if (controlPanel.isRotating == false) return;
                controlPanel.StopRotate();
                break;
            case ButtonType.Forward:
                if (controlPanel.movingForward == false) return;
                controlPanel.StopMoveForward();
                break;
        }
    }
}