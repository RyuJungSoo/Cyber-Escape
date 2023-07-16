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

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //ESC �������� ���� UIŰ��
        if (Input.GetKeyDown(KeyCode.Escape))
            PuzzleUI.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�����Ÿ� �������� ���� UI �ѱ�
        if (collision.CompareTag("Player"))
        {
            PuzzleUI.SetActive(true);
            Timer.SetActive(true);
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
        if (collision.CompareTag("Player"))
        {
            PuzzleUI.SetActive(false);
        }
    }
}
