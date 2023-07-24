using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPuzzleComponent : MonoBehaviour
{
    public int ClickCnt;
    public int Cur_ClickCnt;
    public float LimitTime;
    public Sprite idle_img;
    public Sprite push_img;

    private Image image;
    private PuzzleCompononent puzzleCompononent;
    private GameObject puzzleUI;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        puzzleCompononent = transform.parent.gameObject.GetComponent<PuzzleCompononent>();
        puzzleUI = transform.parent.gameObject.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (LimitTime <= 0) // 시간 제한이 다 되면 종료
        {
            puzzleCompononent.isFailed = true;
            puzzleUI.SetActive(false);
        }
    }

    public void ButtonDown()
    {
        //Debug.Log("Button Down");
        image.sprite = push_img;
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

    public void ButtonUp()
    {
        //Debug.Log("Button Up");
        image.sprite = idle_img;
    }
}
