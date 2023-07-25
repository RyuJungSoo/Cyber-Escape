using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizButtonComponent : MonoBehaviour
{
    public bool isAnswer;

    private GameObject puzzleUI;
    private PuzzleCompononent puzzleCompononent;

    // Start is called before the first frame update
    void Start()
    {
        puzzleUI = transform.parent.gameObject.transform.parent.gameObject;
        puzzleCompononent = transform.parent.gameObject.GetComponent<PuzzleCompononent>();
    }

    public void OnClick()
    {
        if (isAnswer == true)
        {
            puzzleCompononent.isSolved = true;
            puzzleUI.active = false;
        }

        else
        {
            puzzleCompononent.isFailed = true;
            puzzleUI.active = false;
        }
    
    }
}
