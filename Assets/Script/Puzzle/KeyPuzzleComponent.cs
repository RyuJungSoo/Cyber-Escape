using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class KeyPuzzleComponent : MonoBehaviour
{

    public AudioClip[] audio;
    public GameObject[] PuzzleType;
    private Transform[] KeyImgs;
    //public float LimitTime;

    private int index;
    private int Answer_cnt;
    private int Correct_cnt = 0;
    private AudioSource audioSource;

    private void Awake()
    {
        // 문제를 푼 이력이 있는 경우 리셋
        if (gameObject.transform.GetChild(0).GetComponent<PuzzleCompononent>().isSolved == true || gameObject.transform.GetChild(0).GetComponent<PuzzleCompononent>().isFailed == true)
        {
            Reset();
        }

        // 퍼즐 유형 중 하나 가져오기
        index = Random.Range(0, PuzzleType.Length);
        PuzzleType[index].SetActive(true);

        // 답 가져오기
        GameObject output = PuzzleType[index].transform.GetChild(1).gameObject;
        KeyImgs = output.transform.GetComponentsInChildren<Transform>().Where(child => child != output.transform).ToArray();


        foreach (Transform KeyImg in KeyImgs)
            KeyImg.gameObject.SetActive(false);

        // 답 갯수 저장
        Answer_cnt = KeyImgs.Length;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        /*if (LimitTime <= 0) // 시간 제한이 다 되면 종료
        {
            gameObject.GetComponent<PuzzleCompononent>().isFailed = true;
            gameObject.SetActive(false);
        }*/

        
        KeyCompare(KeyInput()); // 키 입력 후, 맞힌 유무에 따라 효과음 재생


        if (Correct_cnt == Answer_cnt) // 맞춘 경우
        {
            gameObject.transform.GetChild(0).GetComponent<PuzzleCompononent>().isSolved = true;
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

        if (KeyImgs[Correct_cnt].gameObject.CompareTag("Up") && InputKeyName == "UpArrow")
            KeyImgs[Correct_cnt].gameObject.active = true;
        else if (KeyImgs[Correct_cnt].gameObject.CompareTag("Down") && InputKeyName == "DownArrow")
            KeyImgs[Correct_cnt].gameObject.active = true;
        else if (KeyImgs[Correct_cnt].gameObject.CompareTag("Left") && InputKeyName == "LeftArrow")
            KeyImgs[Correct_cnt].gameObject.active = true;
        else if (KeyImgs[Correct_cnt].gameObject.CompareTag("Right") && InputKeyName == "RightArrow")
            KeyImgs[Correct_cnt].gameObject.active = true;
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

    private void Reset()
    {

        PuzzleType[index].SetActive(false);
        gameObject.transform.GetChild(0).GetComponent<PuzzleCompononent>().isSolved = false;
        gameObject.transform.GetChild(0).GetComponent<PuzzleCompononent>().isFailed = false;
    }
}
