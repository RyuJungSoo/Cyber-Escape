using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumSpace;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static SoundManager Instance = null; // 어디서든 접근할 수 있도록 인스턴스 선언
    public AudioSource audioSource;
    public AudioClip[] audioClips;

    private void Awake()
    {
        if (Instance) // 이미 Instance에 할당된게 있으면 중복되는 걸 지우기
        {
            DestroyImmediate(this.gameObject);
            return;
        }
        Instance = this; // Instance 할당이 안 되어 있는 경우 현재 것을 할당
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AudioPlay(SoundType type)
    {
        audioSource.PlayOneShot(audioClips[(int)type]);
    }
}
