using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle3x3Component : MonoBehaviour
{
    public PieceCheckComponent pieceCheckComponent;
    public float Interval_Time; // 정답 보여주는 시간 텀

    private int index;
    IEnumerator myCoroutine;


    public void ShowStart()
    {
        Debug.Log("Show");
        myCoroutine = ShowAnswers(Interval_Time);
        StartCoroutine(myCoroutine);
    }

    IEnumerator ShowAnswers(float time)

    {
        int index = 0;
        while (index < pieceCheckComponent.answers.Length)
        {

            yield return new WaitForSeconds(time);
            pieceCheckComponent.answers[index].gameObject.SetActive(true);
            index++;
        }
        Invoke("ShowStop", time);
    }

    void ShowStop()
    {
        foreach (Transform answer in pieceCheckComponent.answers)
        {
            answer.gameObject.SetActive(false);
        }

        foreach (PuzzlePiece piece in pieceCheckComponent.pieces)
        {
            piece.gameObject.SetActive(true);
        }
    }

}
