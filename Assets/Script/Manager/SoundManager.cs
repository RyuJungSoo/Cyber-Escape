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
