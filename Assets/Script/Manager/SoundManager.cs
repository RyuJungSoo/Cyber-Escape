using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumSpace;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static SoundManager Instance = null; // ��𼭵� ������ �� �ֵ��� �ν��Ͻ� ����
    public AudioSource audioSource;
    public AudioClip[] audioClips;

    bool isPressSoundPlaying = false;

    GameObject Player;

    private void Awake()
    {
        if (Instance) // �̹� Instance�� �Ҵ�Ȱ� ������ �ߺ��Ǵ� �� �����
        {
            DestroyImmediate(this.gameObject);
            return;
        }
        Instance = this; // Instance �Ҵ��� �� �Ǿ� �ִ� ��� ���� ���� �Ҵ�
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.Stage == 0 && Player.transform.position.x >= 30 && Player.transform.position.x <= 36.3 && !isPressSoundPlaying)
            PressSoundPlay();

        else if (GameManager.Instance.Stage == 0 && (Player.transform.position.x < 30 || Player.transform.position.x > 36.3) && audioSource.clip == audioClips[6])
            PressSoundStop();

        if (GameManager.Instance.Stage == 1 && Player.transform.position.x >= 63 && Player.transform.position.x <= 84 && !isPressSoundPlaying)
            PressSoundPlay();
        
        else if (GameManager.Instance.Stage == 1 && (Player.transform.position.x < 63 || Player.transform.position.x > 84) && audioSource.clip == audioClips[6]) 
            PressSoundStop();

        if (GameManager.Instance.Stage == 2 && Player.transform.position.x >= 136 && Player.transform.position.x <= 149 && !isPressSoundPlaying)
            PressSoundPlay();

        else if ((Player.transform.position.x < 136 || Player.transform.position.x > 149) && audioSource.clip == audioClips[6] && GameManager.Instance.Stage == 2)
            PressSoundStop();

        if (GameManager.Instance.Stage == 3 && Player.transform.position.x >= 200  && Player.transform.position.x <= 214 && !isPressSoundPlaying)
            PressSoundPlay();

        else if ((Player.transform.position.x < 200 || Player.transform.position.x > 214) && audioSource.clip == audioClips[6] && GameManager.Instance.Stage == 3)
            PressSoundStop();
    }

    public void AudioPlay(SoundType type)
    {
        audioSource.PlayOneShot(audioClips[(int)type]);
    }

    public void PressSoundPlay()
    {
        audioSource.clip = audioClips[6];
        audioSource.loop = true;
        audioSource.Play();
        audioSource.volume = 1.2f;
        isPressSoundPlaying = true;
    }

    public void PressSoundStop()
    {
        isPressSoundPlaying = false;
        audioSource.Stop();
        audioSource.loop = false;
        audioSource.clip = null;
    }
   
}
