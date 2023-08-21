using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DoorButtonController : MonoBehaviour
{
    // Start is called before the first frame update
    //������ UI
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

        //ESC �������� ���� UIŰ��
        /*if (Input.GetKeyDown(KeyCode.Escape))
        {
            PuzzleUI.SetActive(false);
            GameManager.Instance.isBossPuzzleUION = false;
            GameObject.Find("Player").GetComponent<PlayerController>().isPuzzleSolving = false;
        }*/
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�����Ÿ� �������� ���� UI �ѱ�
        if (collision.CompareTag("Player") && !PuzzleUI.transform.GetChild(0).GetComponent<PuzzleCompononent>().isSolved && !PuzzleUI.transform.GetChild(0).GetComponent<PuzzleCompononent>().isFailed)
        {
            collision.gameObject.GetComponent<AudioSource>().Stop();
            collision.gameObject.GetComponent<PlayerController>().isPuzzleSolving = true;
            Timer.SetActive(true);
            PuzzleUI.SetActive(true);

            GameManager.Instance.isBossPuzzleUION = true;
            

            // �̹� Ÿ�̸� �����ߴ��� �ߺ� ���� ����
            if (!isAlreadyTimerSetting)
            {
                //Ÿ�̸� ���� 
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


            // �̹� Ÿ�̸� �����ߴ��� �ߺ� ���� ����
            if (!isAlreadyTimerSetting)
            {
                //Ÿ�̸� ���� 
                Timer.GetComponent<TimerController>().SetTimer(gameObject, limitTime);
                isAlreadyTimerSetting = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        //�����Ÿ����� �������� ���� UI ����
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
