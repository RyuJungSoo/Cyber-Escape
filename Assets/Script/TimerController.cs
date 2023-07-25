using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class TimerController : MonoBehaviour
{
    public Text[] timeText;
    public float time = 60;
    public float originTime = 0;
    public GameObject doorButton;
    public GameObject player;

    public int min, sec;

    public bool isTimeOver = false;
    public bool timerStop = true;

    // Start is called before the first frame update
    void Start()
    {
        originTime = time;
        timeText[0].text = ((int)time / 60).ToString();
        timeText[1].text = (((int)time - min * 60) % 60).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (timerStop) return;

        time -= Time.deltaTime;
        
        // 분,초 설정
        min =  (int)time / 60;
        sec = ((int)time - min * 60) % 60 ;


        //만약 문제를 풀었으면
        if (doorButton.GetComponent<DoorButtonController>().PuzzleUI.GetComponent<PuzzleCompononent>().isSolved)
        {
            //타이머 정지
            timerStop = true;
            player.GetComponent<PlayerController>().isPuzzleSolving = false;
            doorButton.GetComponent<DoorButtonController>().PuzzleUI.GetComponent<PuzzleCompononent>().isSolved = true;
            doorButton.GetComponent<DoorButtonController>().PuzzleUI.GetComponent<PuzzleCompononent>().isFailed = false;

            gameObject.SetActive(false); 
        }

        // 타이머가 끝나면
        if (min == 0 && sec == 0 && !isTimeOver)
        {
            min = 0;
            sec = 0;
            //문 열리는 애니메이션 재생
            doorButton.GetComponent<DoorButtonController>().Door.GetComponent<Animator>().SetBool("isDoorOpen", true);
            doorButton.GetComponent<DoorButtonController>().PuzzleUI.GetComponent<PuzzleCompononent>().isSolved = false;
            doorButton.GetComponent<DoorButtonController>().PuzzleUI.GetComponent<PuzzleCompononent>().isFailed = true;
            //몬스터 스폰 시작
            doorButton.GetComponent<DoorButtonController>().Door.GetComponent<MonsterSpawnComponent>().isStartSpawn = true;


            //퍼즐 UI 숨기기
            doorButton.GetComponent<DoorButtonController>().PuzzleUI.SetActive(false);
            isTimeOver = true;
            timerStop = true;
            player.GetComponent<PlayerController>().isPuzzleSolving = false;
            gameObject.SetActive(false);
        }
        
        //UI 텍스트 설정
        timeText[0].text = min.ToString();
        timeText[1].text = sec.ToString();
        
    }

    public void SetTimer(GameObject doorButton, float time)
    {
        this.doorButton = doorButton;
        this.time = time;
        originTime = time;
        isTimeOver = false;
        timerStop = false;
    }

    
}
