using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButtonComponent : MonoBehaviour
{
    public GameObject Logo;
    public GameObject Log;


    public void OnClick()
    {

        StartCoroutine("FadeOut");
    }

    IEnumerator FadeOut()
    {

        Image StartButtonImage = this.gameObject.GetComponent<Image>();
        

        while (StartButtonImage.color.a > 0f)
        {
            StartButtonImage.color = new Color(StartButtonImage.color.r, StartButtonImage.color.g, StartButtonImage.color.b, StartButtonImage.color.a - Time.deltaTime);
            yield return null;
        }

        Logo.SetActive(false);
        this.gameObject.SetActive(false);
        Log.SetActive(true);
    }
}
