using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TangramComponent : MonoBehaviour
{
    public GameObject[] pieces;
    private int cnt = 0;

    public void SetCheck()
    {
        foreach (GameObject piece in pieces)
        {
            if (piece.GetComponent<PuzzlePiece>().isSet == true)
                cnt++;
        }
        //Debug.Log(cnt);

        if (cnt == pieces.Length)
        {
            GetComponent<PuzzleCompononent>().isSolved = true;
            transform.parent.gameObject.SetActive(false);
        }
        else
            cnt = 0;
    }
}
