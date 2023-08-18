using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class EndingCredit : MonoBehaviour
{
    private bool isFadeEnd;
    private bool isLog;
    private bool isLogEnd;
    private bool isEnd;
    private int index;
    public GameObject[] Logs;
    public GameObject[] Credits;
    public GameObject Click;

    void Start()
    {
        
        StartCoroutine("FadeIn");
    }

    void Update()
    {
        if (isEnd == true)
        {
            
            if (Input.GetKeyDown(KeyCode.Mouse0))
                SceneManager.LoadScene(0);
            return;
        }

        if (isFadeEnd == true && isLog == false && isLogEnd == false)
        {
            StartCoroutine(StartLog(index));
            index++;

            if (index >= Logs.Length)
            {
                isLogEnd = true;
                index = 0;
            }
        }

        else if (isFadeEnd == true && isLog == false && isLogEnd == true && isEnd == false)
        {
            
            StartCoroutine(StartCredit(index));
            index++;
            

        }

    }


    IEnumerator FadeIn()
    {
        Image Image = this.gameObject.GetComponent<Image>();


        while (Image.color.a < 1)
        {
            Image.color = new Color(Image.color.r, Image.color.g, Image.color.b, Image.color.a + Time.deltaTime);
            yield return null;
        }
        isFadeEnd = true;
    }

    IEnumerator StartLog(int index)
    {

        isLog = true;
        yield return new WaitForSeconds(1f);

        Logs[index].SetActive(true);
        TMP_Text text = Logs[index].GetComponent<TMP_Text>();

        while (text.color.a < 1)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(2);

        while (text.color.a > 0)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - Time.deltaTime);
            yield return null;
        }

        Logs[index].SetActive(false);
        isLog = false;
    }

    IEnumerator StartCredit(int index)
    {
        isLog = true;
        yield return new WaitForSeconds(0.7f);

        Credits[index].SetActive(true);
        TMP_Text text = Credits[index].GetComponent<TMP_Text>();

        while (text.color.a < 1)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(2);

        isLog = false;
        if (index >= Credits.Length - 1)
        {
            Click.SetActive(true);
            isEnd = true;

        }
    }
}
