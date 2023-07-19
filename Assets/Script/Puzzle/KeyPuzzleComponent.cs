using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPuzzleComponent : MonoBehaviour
{
    public bool isFailed; // ���� ����
    public bool isSolved; // �ذ� ����
    public AudioClip[] audio;
    public GameObject[] KeyImg;
    public float LimitTime;

    private int Answer_cnt;
    private int Correct_cnt = 0;
    private AudioSource audioSource;

    private void Awake()
    {
        if (isFailed == true) // �̹� ������ ����ó�� 
        {
            this.gameObject.active = false;
        }
        Answer_cnt = KeyImg.Length;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        if (LimitTime <= 0) // �ð� ������ �� �Ǹ� ����
        {
            gameObject.GetComponent<PuzzleCompononent>().isFailed = true;
            isFailed = true;
            gameObject.SetActive(false);
        }

        
        KeyCompare(KeyInput()); // Ű �Է� ��, ���� ������ ���� ȿ���� ���


        if (Correct_cnt == Answer_cnt) // ���� ���
        {
            gameObject.GetComponent<PuzzleCompononent>().isSolved = true;
            isSolved = true;
            gameObject.SetActive(false);
        }
    }



    string KeyInput() // Ű �Է� 
    {
        string keyName = null; // �Էµ� Ű �̸��� ������ ����
        if (Input.anyKeyDown)
        {
            foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(keyCode)) // �ش� Ű�� ������ �� ���� Ű�� �̸��� ��ȯ
                {
                    keyName = keyCode.ToString();
                    break;
                }
            }
        }
        return keyName;

    }

    void KeyCompare(string InputKeyName) // Ű �Է� Ȯ��
    {

        if (InputKeyName == null) // �Է��� ���� ���
            return;

        if (KeyImg[Correct_cnt].CompareTag("Up") && InputKeyName == "UpArrow")
            KeyImg[Correct_cnt].active = true;
        else if (KeyImg[Correct_cnt].CompareTag("Down") && InputKeyName == "DownArrow")
            KeyImg[Correct_cnt].active = true;
        else if (KeyImg[Correct_cnt].CompareTag("Left") && InputKeyName == "LeftArrow")
            KeyImg[Correct_cnt].active = true;
        else if (KeyImg[Correct_cnt].CompareTag("Right") && InputKeyName == "RightArrow")
            KeyImg[Correct_cnt].active = true;
        else // Ʋ�� �Է��� ���
        {
            SFXAudio(false);
            return;
        }

        Correct_cnt++;
        SFXAudio(true);
    }


    void SFXAudio(bool isCorrect) // ȿ���� ���
    {

        if(isCorrect == true)
            audioSource.clip = audio[1];
        else if (isCorrect == false)
            audioSource.clip = audio[0];
        audioSource.Play();
    }
}
