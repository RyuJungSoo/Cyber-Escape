using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LogComponent : MonoBehaviour
{
    private bool isFadeEnd;
    private int index = 0;
    public GameObject[] Logs;

    private void Awake()
    {
        StartCoroutine("FadeIn");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isFadeEnd)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (index < Logs.Length)
                {
                    Logs[index].SetActive(true);
                    index++;
                }
                else
                {
                    isFadeEnd = false;
                    StartCoroutine("TextFadeOut");
                }
            }
        }
    }



    IEnumerator FadeIn()
    {

        Image Image = this.gameObject.GetComponent<Image>();


        while (Image.color.a < 145f / 255f)
        {
            Image.color = new Color(Image.color.r, Image.color.g, Image.color.b, Image.color.a + Time.deltaTime);
            yield return null;
        }
        Logs[index].SetActive(true);
        index++;
        isFadeEnd = true;
    }

    IEnumerator TextFadeOut()
    {
        
        while (Logs[Logs.Length-1].GetComponent<TMP_Text>().color.a > 0)
        {
            //Debug.Log("Start");
            foreach (GameObject Log in Logs)
            {
                Color Textcolor = Log.GetComponent<TMP_Text>().color;
                Log.GetComponent<TMP_Text>().color = new Color(Textcolor.r, Textcolor.g, Textcolor.b, Textcolor.a - Time.deltaTime);
            }
            yield return null;
        }
        StartCoroutine("ToGameScene");
    }

    IEnumerator ToGameScene()
    {

        Image Image = this.gameObject.GetComponent<Image>();


        while (Image.color.a < 1)
        {
            Image.color = new Color(Image.color.r, Image.color.g, Image.color.b, Image.color.a + Time.deltaTime);
            yield return null;
        }
        SceneManager.LoadScene(1);
    }
}
