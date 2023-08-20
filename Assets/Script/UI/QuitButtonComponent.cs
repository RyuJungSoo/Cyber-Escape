using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButtonComponent : MonoBehaviour
{
    public void GameExit()
    {
        Application.Quit();
    }
}
