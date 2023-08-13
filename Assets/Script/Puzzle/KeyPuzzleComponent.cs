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
        // ������ Ǭ �̷��� �ִ� ��� ����
        if (gameObject.transform.GetChild(0).GetComponent<PuzzleCompononent>().isSolved == true || gameObject.transform.GetChild(0).GetComponent<PuzzleCompononent>().isFailed == true)
        {
            Reset();
        }

        // ���� ���� �� �ϳ� ��������
        index = Random.Range(0, PuzzleType.Length);
        PuzzleType[index].SetActive(true);

        // �� ��������
        GameObject output = PuzzleType[index].transform.GetChild(1).gameObject;
        KeyImgs = output.transform.GetComponentsInChildren<Transform>().Where(child => child != output.transform).ToArray();


        foreach (Transform KeyImg in KeyImgs)
            KeyImg.gameObject.SetActive(false);

        // �� ���� ����
        Answer_cnt = KeyImgs.Length;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        /*if (LimitTime <= 0) // �ð� ������ �� �Ǹ� ����
        {
            gameObject.GetComponent<PuzzleCompononent>().isFailed = true;
            gameObject.SetActive(false);
        }*/

        
        KeyCompare(KeyInput()); // Ű �Է� ��, ���� ������ ���� ȿ���� ���


        if (Correct_cnt == Answer_cnt) // ���� ���
        {
            gameObject.transform.GetChild(0).GetComponent<PuzzleCompononent>().isSolved = true;
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

        if (KeyImgs[Correct_cnt].gameObject.CompareTag("Up") && InputKeyName == "UpArrow")
            KeyImgs[Correct_cnt].gameObject.active = true;
        else if (KeyImgs[Correct_cnt].gameObject.CompareTag("Down") && InputKeyName == "DownArrow")
            KeyImgs[Correct_cnt].gameObject.active = true;
        else if (KeyImgs[Correct_cnt].gameObject.CompareTag("Left") && InputKeyName == "LeftArrow")
            KeyImgs[Correct_cnt].gameObject.active = true;
        else if (KeyImgs[Correct_cnt].gameObject.CompareTag("Right") && InputKeyName == "RightArrow")
            KeyImgs[Correct_cnt].gameObject.active = true;
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

    private void Reset()
    {

        PuzzleType[index].SetActive(false);
        gameObject.transform.GetChild(0).GetComponent<PuzzleCompononent>().isSolved = false;
        gameObject.transform.GetChild(0).GetComponent<PuzzleCompononent>().isFailed = false;
    }
}
