using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButtonComponent : MonoBehaviour
{
    public bool isPauseButton;


    private void Update()
    {
        if (isPauseButton == false)
            return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnClick();
        }
    }

    public void OnClick()
    {

        if (UiManager.Instance.PauseUI.active == false)
        {
            UiManager.Instance.PauseUI_On();
            
        }
        else
        {
            UiManager.Instance.PauseUI_Off();
            
        }
    }
}
