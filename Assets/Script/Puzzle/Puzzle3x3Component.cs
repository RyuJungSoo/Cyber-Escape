using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle3x3Component : MonoBehaviour
{
    public GameObject[] answers;
    public GameObject[] pieces;
    public float Interval_Time; // 정답 보여주는 시간 텀

    private int index;
    IEnumerator myCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        //myCoroutine = ShowAnswers();
        //StartCoroutine(myCoroutine);
    }

    // Update is called once per frame
    void Update()
    {
        if (index >= answers.Length)
        {
            StopCoroutine("ShowAnswers");
            Invoke("ShowStop", 2f);
        }
        else
            StartCoroutine("ShowAnswers");
        
    }

    IEnumerator ShowAnswers()
    {

        yield return new WaitForSeconds(Interval_Time);
        answers[index].SetActive(true);
        index++;
        
    }

    void ShowStop()
    {
        foreach (GameObject answer in answers)
        {
            answer.SetActive(false);
        }

        foreach (GameObject piece in pieces)
        {
            piece.SetActive(true);
        }
    }

}
