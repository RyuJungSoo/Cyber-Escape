using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPuzzleComponent : MonoBehaviour
{
    public int ClickCnt;
    public int Cur_ClickCnt;
    public float LimitTime;


    private PuzzleCompononent puzzleCompononent;
    private GameObject puzzleUI;
    // Start is called before the first frame update
    void Start()
    {
        puzzleCompononent = transform.parent.gameObject.GetComponent<PuzzleCompononent>();
        puzzleUI = transform.parent.gameObject.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (LimitTime <= 0) // �ð� ������ �� �Ǹ� ����
        {
            puzzleCompononent.isFailed = true;
            puzzleUI.SetActive(false);
        }
    }

    public void OnClick()
    {

        Cur_ClickCnt++;
        if (Cur_ClickCnt == ClickCnt)
        {
            puzzleCompononent.isSolved = true;
            puzzleUI.SetActive(false);
        }
    }
}
