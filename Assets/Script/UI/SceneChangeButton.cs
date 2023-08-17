using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeButton : MonoBehaviour
{
    public int index;
    public void OnClick()
    {
        SceneManager.LoadScene(index);
    }
}
