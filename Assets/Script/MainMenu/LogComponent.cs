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
    public GameObject ClickLog;
    public string[] Texts;
    private bool isTyping;
    private AudioSource audioSource;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine("FadeIn");
    }

    // Update is called once per frame
    void Update()
    {
        if (isFadeEnd && isTyping == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (index < Logs.Length)
                {
                    Logs[index].SetActive(true);
                    StartCoroutine(Typing(index));
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
        ClickLog.SetActive(true);
        isFadeEnd = true;
    }

    IEnumerator Typing(int index)
    {
        audioSource.Play();
        isTyping = true;
        for (int i = 0; i <= Texts[index].Length; i++)
        {

            //Debug.Log(Texts[index].Substring(0, i));
            Logs[index].GetComponent<TMP_Text>().text = Texts[index].Substring(0, i);
            yield return new WaitForSeconds(0.1f);

        }
        isTyping = false;
        audioSource.Stop();
    }

    IEnumerator TextFadeOut()
    {
        ClickLog.SetActive(false);
        while (Logs[Logs.Length - 1].GetComponent<TMP_Text>().color.a > 0)
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

