using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle3x3Component : MonoBehaviour
{
    public GameObject[] answers;
    public GameObject[] pieces;
    public float Interval_Time; // ���� �����ִ� �ð� ��

    private int index;
    IEnumerator myCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        myCoroutine = ShowAnswers(Interval_Time);
        StartCoroutine(myCoroutine);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ShowAnswers(float time)
    {
        int index = 0;

        while (index < answers.Length)
        {
            yield return new WaitForSeconds(time);
            answers[index].SetActive(true);
            index++;
        }
        Invoke("ShowStop", time);
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
