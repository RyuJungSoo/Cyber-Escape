using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipButtonComponent : MonoBehaviour
{
    public LogComponent logComponent;
    public void Skip()
    {
        logComponent.SoundStop();
        logComponent.isSkip = true;
        SceneManager.LoadScene(1);
    }
}
