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

    public bool isBossDoorButton = false;
    public bool isNoDoorButton = false;


    public GameObject[] BossPuzzles;
    public BossComponent boss;
    public GameObject transporter;

    void Start()
    {
        if (isBossDoorButton)
        {
            PuzzleUI = BossPuzzles[Random.Range(0, 3)];
        }
    }

    // Update is called once per frame
    void Update()
    {

        //ESC 눌렀을때 퍼즐 UI키기
        /*if (Input.GetKeyDown(KeyCode.Escape))
        {
            PuzzleUI.SetActive(false);
            GameManager.Instance.isBossPuzzleUION = false;
            GameObject.Find("Player").GetComponent<PlayerController>().isPuzzleSolving = false;
        }*/
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

            GameManager.Instance.isBossPuzzleUION = true;
            

            // 이미 타이머 세팅했는지 중복 세팅 방지
            if (!isAlreadyTimerSetting)
            {
                //타이머 설정 
                Timer.GetComponent<TimerController>().SetTimer(gameObject, limitTime);
                isAlreadyTimerSetting = true;
            }
        }

        if (collision.CompareTag("Player") && isBossDoorButton)
        {
            collision.gameObject.GetComponent<AudioSource>().Stop();
            collision.gameObject.GetComponent<PlayerController>().isPuzzleSolving = true;
            Timer.SetActive(true);
            PuzzleUI.SetActive(true);

            GameManager.Instance.isBossPuzzleUION = true;
            transporter.GetComponent<Transporter>().TransporterStop();


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
        if (collision.CompareTag("Player") && !isBossDoorButton)
        {
            PuzzleUI.SetActive(false);
            GameManager.Instance.isBossPuzzleUION = false;
        }
    }

    public void Reset()
    {
        if (isBossDoorButton)
        {
            int randomIdx = Random.Range(0, 3);
            PuzzleUI = BossPuzzles[randomIdx];

            if (randomIdx > 0)
            {
                
                PuzzleUI.transform.GetChild(0).GetComponent<PieceCheckComponent>().Reset();
                
            }

            isAlreadyTimerSetting = false;
        }
    }
}
