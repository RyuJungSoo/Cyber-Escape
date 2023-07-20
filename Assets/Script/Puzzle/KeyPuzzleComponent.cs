using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPuzzleComponent : MonoBehaviour
{
    public bool isFailed; // 실패 유무
    public bool isSolved; // 해결 유무
    public AudioClip[] audio;
    public GameObject[] KeyImg;
    public float LimitTime;

    private int Answer_cnt;
    private int Correct_cnt = 0;
    private AudioSource audioSource;

    private void Awake()
    {
        if (isFailed == true) // 이미 실패한 예외처리 
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

        if (LimitTime <= 0) // 시간 제한이 다 되면 종료
        {
            gameObject.GetComponent<PuzzleCompononent>().isFailed = true;
            isFailed = true;
            gameObject.SetActive(false);
        }

        
        KeyCompare(KeyInput()); // 키 입력 후, 맞힌 유무에 따라 효과음 재생


        if (Correct_cnt == Answer_cnt) // 맞춘 경우
        {
            gameObject.GetComponent<PuzzleCompononent>().isSolved = true;
            isSolved = true;
            gameObject.SetActive(false);
        }
    }



    string KeyInput() // 키 입력 
    {
        string keyName = null; // 입력된 키 이름을 저장할 변수
        if (Input.anyKeyDown)
        {
            foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(keyCode)) // 해당 키가 눌렸을 때 눌린 키의 이름을 반환
                {
                    keyName = keyCode.ToString();
                    break;
                }
            }
        }
        return keyName;

    }

    void KeyCompare(string InputKeyName) // 키 입력 확인
    {

        if (InputKeyName == null) // 입력이 없는 경우
            return;

        if (KeyImg[Correct_cnt].CompareTag("Up") && InputKeyName == "UpArrow")
            KeyImg[Correct_cnt].active = true;
        else if (KeyImg[Correct_cnt].CompareTag("Down") && InputKeyName == "DownArrow")
            KeyImg[Correct_cnt].active = true;
        else if (KeyImg[Correct_cnt].CompareTag("Left") && InputKeyName == "LeftArrow")
            KeyImg[Correct_cnt].active = true;
        else if (KeyImg[Correct_cnt].CompareTag("Right") && InputKeyName == "RightArrow")
            KeyImg[Correct_cnt].active = true;
        else // 틀린 입력인 경우
        {
            SFXAudio(false);
            return;
        }

        Correct_cnt++;
        SFXAudio(true);
    }


    void SFXAudio(bool isCorrect) // 효과음 재생
    {

        if(isCorrect == true)
            audioSource.clip = audio[1];
        else if (isCorrect == false)
            audioSource.clip = audio[0];
        audioSource.Play();
    }
}
