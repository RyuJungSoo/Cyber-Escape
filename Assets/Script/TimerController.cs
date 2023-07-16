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
        
        // ��,�� ����
        min =  (int)time / 60;
        sec = ((int)time - min * 60) % 60 ;


        //���� ������ Ǯ������
        if (doorButton.GetComponent<DoorButtonController>().PuzzleUI.GetComponent<PuzzleCompononent>().isSolved)
        {
            //Ÿ�̸� ����
            timerStop = true;
            gameObject.SetActive(false); 
        }

        // Ÿ�̸Ӱ� ������
        if (min == 0 && sec == 0 && !isTimeOver)
        {
            min = 0;
            sec = 0;
            //�� ������ �ִϸ��̼� ���
            doorButton.GetComponent<DoorButtonController>().Door.GetComponent<Animator>().SetBool("isDoorOpen", true);
            //���� UI �����
            doorButton.GetComponent<DoorButtonController>().PuzzleUI.SetActive(false);
            isTimeOver = true;
            timerStop = true;
            gameObject.SetActive(false);
        }
        
        //UI �ؽ�Ʈ ����
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
