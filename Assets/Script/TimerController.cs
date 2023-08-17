using EnumSpace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
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
    public bool isBossStage = false;

    float overSoundTimer = 0.0f;

    AudioSource audioSource;
    public AudioClip[] audioClips;

    public Transporter transporter;

    // Start is called before the first frame update
    void Start()
    {

        audioSource = GetComponent<AudioSource>();

        originTime = time;
        timeText[0].text = ((int)time / 60).ToString();
        timeText[1].text = (((int)time - min * 60) % 60).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.Stage == 4)
            isBossStage = true;

        if (timerStop) return;

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(audioClips[0]);
        }

        time -= Time.deltaTime;
        
        // ��,�� ����
        min =  (int)time / 60;
        sec = ((int)time - min * 60) % 60 ;


        //���� ������ Ǯ������
        if (doorButton.GetComponent<DoorButtonController>().PuzzleUI.transform.GetChild(0).GetComponent<PuzzleCompononent>().isSolved)
        {
            //Ÿ�̸� ����
            timerStop = true;
            player.GetComponent<PlayerController>().isPuzzleSolving = false;
            doorButton.GetComponent<DoorButtonController>().PuzzleUI.transform.GetChild(0).GetComponent<PuzzleCompononent>().isSolved = true;
            doorButton.GetComponent<DoorButtonController>().PuzzleUI.transform.GetChild(0).GetComponent<PuzzleCompononent>().isFailed = false;
            audioSource.Stop();
            SoundManager.Instance.AudioPlay(SoundType.PUZZLECORRECT);

            if (isBossStage)
            {
                transporter.TransporterStop();
                GameManager.Instance.isBossPuzzleUION = false;
                doorButton.SetActive(false);
            }

            gameObject.SetActive(false); 
        }

        // Ÿ�̸Ӱ� ������
        if (min == 0 && sec == 0 && !isTimeOver)
        {
            min = 0;
            sec = 0;
            audioSource.Stop();
            SoundManager.Instance.AudioPlay(SoundType.TIMEOVER);
            overSoundTimer += Time.deltaTime;

            //�� ������ �ִϸ��̼� ���
                SoundManager.Instance.AudioPlay(SoundType.DOOROPEN);

            if (!isBossStage)
            {
                doorButton.GetComponent<DoorButtonController>().Door.GetComponent<Animator>().SetBool("isDoorOpen", true);
                doorButton.GetComponent<DoorButtonController>().Door.GetComponent<MonsterSpawnComponent>().isStartSpawn = true;
            }
            else
            {
                transporter.TransporterStart();
                GameManager.Instance.isBossPuzzleUION = false;
                doorButton.SetActive(false);
            }
           
            doorButton.GetComponent<DoorButtonController>().PuzzleUI.transform.GetChild(0).GetComponent<PuzzleCompononent>().isSolved = false;
            doorButton.GetComponent<DoorButtonController>().PuzzleUI.transform.GetChild(0).GetComponent<PuzzleCompononent>().isFailed = true;
            
            //���� UI �����
            doorButton.GetComponent<DoorButtonController>().PuzzleUI.SetActive(false);
            isTimeOver = true;
            timerStop = true;
            player.GetComponent<PlayerController>().isPuzzleSolving = false;
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
        overSoundTimer = 0f;
    }

    
}
