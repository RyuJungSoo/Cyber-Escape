using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DoorButtonController : MonoBehaviour
{
    // Start is called before the first frame update
    //보여줄 UI
    public GameObject PuzzleUI;
    public GameObject Timer;
    public GameObject Door;

    public float limitTime = 60f;
    bool isAlreadyTimerSetting = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //ESC 눌렀을때 퍼즐 UI키기
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PuzzleUI.SetActive(false);
            GameObject.Find("Player").GetComponent<PlayerController>().isPuzzleSolving = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //일정거리 들어왔을때 퍼즐 UI 켜기
        if (collision.CompareTag("Player") && !PuzzleUI.transform.GetChild(0).GetComponent<PuzzleCompononent>().isSolved && !PuzzleUI.transform.GetChild(0).GetComponent<PuzzleCompononent>().isFailed)
        {
            collision.gameObject.GetComponent<AudioSource>().Stop();
            collision.gameObject.GetComponent<PlayerController>().isPuzzleSolving = true;
            Timer.SetActive(true);
            PuzzleUI.SetActive(true);

            // 이미 타이머 세팅했는지 중복 세팅 방지
            if (!isAlreadyTimerSetting)
            {
                //타이머 설정 
                Timer.GetComponent<TimerController>().SetTimer(gameObject, limitTime);
                isAlreadyTimerSetting = true;

            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        //일정거리에서 나갔을때 퍼즐 UI 끄기
        if (collision.CompareTag("Player"))
        {
            PuzzleUI.SetActive(false);
        }
    }
}
