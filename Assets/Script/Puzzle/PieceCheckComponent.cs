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
    public int index;
    private int cnt = 0;

    private void OnEnable()
    {
        //Debug.Log("enabled");
        PuzzleSetting();
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
            PuzzleType[index].SetActive(false);
            transform.parent.gameObject.SetActive(false);
        }
        else
            cnt = 0;
    }

    public void Reset()
    {
        //  이전 퍼즐이 안 꺼진 경우 초기화
        //if (is3x3)
        /*if (PuzzleType[index].activeSelf == true)
        {
            foreach (PuzzlePiece piece in pieces)
            {
                if (piece.gameObject.GetComponent<PuzzlePiece>().isSet == true)
                    piece.gameObject.transform.SetParent(piece.gameObject.transform.parent.transform.parent);
                piece.gameObject.GetComponent<PuzzlePiece>().isSet = false;

            }
            if (is3x3)
            {

                foreach (Transform answer in answers)
                    answer.gameObject.SetActive(true);
            }

            for (int i = 0; i < pieces.Length; i++)
            {

                pieces[i].gameObject.GetComponent<RectTransform>().anchoredPosition = PiecesOriginPos[i];

            }

            PuzzleType[index].SetActive(false);
            GetComponent<PuzzleCompononent>().isSolved = false;
            GetComponent<PuzzleCompononent>().isFailed = false;
        }*/

        // 문제를 푼 이력이 없는 경우 return
        if (GetComponent<PuzzleCompononent>().isSolved != true && GetComponent<PuzzleCompononent>().isFailed != true)
            return;

            Debug.Log("Reset");

        foreach (PuzzlePiece piece in pieces)
        {
            if(piece.gameObject.GetComponent<PuzzlePiece>().isSet == true)
                piece.gameObject.transform.SetParent(piece.gameObject.transform.parent.transform.parent);
            piece.gameObject.GetComponent<PuzzlePiece>().isSet = false;
            
        }
        if (is3x3)
        {

            foreach (Transform answer in answers)
                answer.gameObject.SetActive(true);
        }
        
        for (int i = 0; i < pieces.Length; i++)
        {

            pieces[i].gameObject.GetComponent<RectTransform>().anchoredPosition = PiecesOriginPos[i];
            
        }
        
        
        PuzzleType[index].SetActive(false);
        GetComponent<PuzzleCompononent>().isSolved = false;
        GetComponent<PuzzleCompononent>().isFailed = false;

    }

    public void PuzzleSetting()
    {
        if (PuzzleType[index].activeSelf == true)
        {
            PuzzleType[index].SetActive(false);
            GetComponent<PuzzleCompononent>().isSolved = false;
            GetComponent<PuzzleCompononent>().isFailed = false;
        }
        // 퍼즐 유형 중 하나 가져오기
        index = Random.Range(0, PuzzleType.Length);
        Debug.Log(index);
        PuzzleType[index].SetActive(true);
        PuzzleType[index].transform.GetChild(0).gameObject.SetActive(true);

        // 퍼즐 피스 가져오기
        pieces = PuzzleType[index].transform.GetChild(0).gameObject.GetComponentsInChildren<PuzzlePiece>();
        
        //초기화 목적을 위해 초기 위치를 저장
        
        PiecesOriginPos = new Vector3[pieces.Length];
        for (int i = 0; i < pieces.Length; i++)
        {
          PiecesOriginPos[i] = pieces[i].gameObject.GetComponent<RectTransform>().anchoredPosition;

        }


        // 퍼즐 정답 가져오기
        if (is3x3)
        {
            GameObject tmp = PuzzleType[index].transform.GetChild(0).gameObject.transform.Find("answer").gameObject;
            answers = tmp.transform.GetComponentsInChildren<Transform>().Where(child => child != tmp.transform).ToArray();

            foreach (PuzzlePiece piece in pieces)
                piece.gameObject.SetActive(false);

            foreach (Transform answer in answers)
                answer.gameObject.SetActive(false);

            PuzzleType[index].transform.GetChild(0).gameObject.GetComponent<Puzzle3x3Component>().ShowStart();
        }
        
    }
}
