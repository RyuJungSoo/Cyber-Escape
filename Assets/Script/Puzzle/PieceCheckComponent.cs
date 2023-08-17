using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class PieceCheckComponent : MonoBehaviour
{
    public bool is3x3;
    public GameObject[] PuzzleType;
    public Transform[] answers;
    public PuzzlePiece[] pieces;
    private Vector3[] PiecesOriginPos;
    private int index;
    private int cnt = 0;

    private void Awake()
    {
 
        // ������ Ǭ �̷��� �ִ� ��� ����
        if (GetComponent<PuzzleCompononent>().isSolved == true || GetComponent<PuzzleCompononent>().isFailed == true)
        {
            Reset();
        }

        // ���� ���� �� �ϳ� ��������
        index = Random.Range(0, PuzzleType.Length);
        PuzzleType[index].SetActive(true);
        PuzzleType[index].transform.GetChild(0).gameObject.SetActive(true);

        // ���� �ǽ� ��������
        pieces = PuzzleType[index].transform.GetChild(0).gameObject.GetComponentsInChildren<PuzzlePiece>();
        if (is3x3 == false) // ĥ�� ������ ���, �ʱ�ȭ ������ ���� �ʱ� ��ġ�� ����
        {
            PiecesOriginPos = new Vector3[pieces.Length];
            for (int i = 0; i < pieces.Length; i++)
            {
                PiecesOriginPos[i] = pieces[i].gameObject.GetComponent<RectTransform>().anchoredPosition;
            }

        }


        // ���� ���� ��������
        if (is3x3)
        {
            GameObject tmp = PuzzleType[index].transform.GetChild(0).gameObject.transform.Find("answer").gameObject;
            answers = tmp.transform.GetComponentsInChildren<Transform>().Where(child => child != tmp.transform).ToArray();

            foreach (PuzzlePiece piece in pieces)
                piece.gameObject.SetActive(false);

            foreach (Transform answer in answers)
                answer.gameObject.SetActive(false);
        }

        
        

    }

    public void SetCheck()
    {
        foreach (PuzzlePiece piece in pieces)
        {
            if (piece.isSet == true)
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

    public void Reset()
    {
        foreach (PuzzlePiece piece in pieces)
            piece.gameObject.transform.SetParent(piece.gameObject.transform.parent.transform.parent);
        if (is3x3)
        {

            foreach (Transform answer in answers)
                answer.gameObject.SetActive(true);
        }
        else
        {
            for (int i = 0; i < pieces.Length; i++)
            {
                
                pieces[i].gameObject.GetComponent<RectTransform>().anchoredPosition = PiecesOriginPos[i];
            }
        }

        PuzzleType[index].SetActive(false);
        GetComponent<PuzzleCompononent>().isSolved = false;
        GetComponent<PuzzleCompononent>().isFailed = false;
    }
}
