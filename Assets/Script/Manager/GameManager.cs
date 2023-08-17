using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    SpriteRenderer renderer;
    SpriteRenderer BossRenderer;
    public static GameManager Instance = null; // 어디서든 접근할 수 있도록 인스턴스 선언
    [SerializeField]private int Stage = 0;
    public Vector2[] Stage_Pos;
    public GameObject Player;
    public GameObject Boss;
    public AudioClip[] bgms;
    private PlayerController playerController;
    public AudioSource playerAudioSource;
    private HitObject hitObject;

    public float Respawn_Time = 1f;


    private void Awake()
    {
        if(Instance) // 이미 Instance에 할당된게 있으면 중복되는 걸 지우기
        {
            DestroyImmediate(this.gameObject);
            return;
        }
        Instance = this; // Instance 할당이 안 되어 있는 경우 현재 것을 할당
        //DontDestroyOnLoad(gameObject);

    }

    // Start is called before the first frame update
    void Start()
    {
        playerController = Player.GetComponent<PlayerController>();
        hitObject = Player.GetComponent<HitObject>();
        renderer = Player.GetComponent<SpriteRenderer>();
        BossRenderer = Boss.GetComponent<SpriteRenderer>();
        UiManager.Instance.HpUI_Update();
    }
    public void Update()
    {
        for (int i = 0; i < Stage_Pos.Length; i++)
        {
            if (Player.transform.position.x >= Stage_Pos[i].x)
                GameManager.Instance.Stage = i;
        }
    }

    public void Boss_On()
    {
        Boss.SetActive(true);
        StartCoroutine("Boss_Setting");
    }


    public float GetPlayerHp()
    {
        return playerController.Hp;
    }

    public float GetPlayerMaxHp()
    {
        return playerController.MaxHp;
    }

    public float GetBossHp()
    {
        return Boss.GetComponent<MonsterComponent>().Hp;
    }

    public float GetBossMaxHp()
    {
        return Boss.GetComponent<MonsterComponent>().MaxHp;
    }


    public void NextStage()
    {
        Stage++;
    }

    public void Respawn()
    {
        Player.SetActive(true);
        Player.transform.position = Stage_Pos[Stage];
        renderer.color = new Color(1,1,1);
        playerController.enabled = true;
        playerController.isDead = false;
        playerController.Hp = playerController.MaxHp;
        UiManager.Instance.HpUI_Update();
    }


    public void PlayerDamage(float Damage,bool isRed) 
    {
        //피격시 0.5초 무적
        if (Player.GetComponent<HitObject>().isChange) return;
        Player.GetComponent<PlayerController>().HitSoundPlay();
        Player.GetComponent<HitObject>().ChangeColor(isRed);

        playerController.Hp -= Damage;

        if (playerController.Hp < 0)
        {
            playerController.Hp = 0;
        }

        UiManager.Instance.HpUI_Update();

        if (playerController.Hp <= 0)
        {
            playerController.isDead = true;
            playerController.enabled = false;
            hitObject.FadeOutStart();

        }
    }


    public void ChangeBGM(int index)
    {
        
        playerAudioSource.clip = bgms[index];
        playerAudioSource.Play();
    }

    IEnumerator Boss_Setting()
    {
        Time.timeScale = 0;
        while (BossRenderer.color.a < 1)
        {
            BossRenderer.color = new Color(BossRenderer.color.r, BossRenderer.color.g, BossRenderer.color.b, BossRenderer.color.a + Time.unscaledDeltaTime);
            yield return null;
        }
        Boss.GetComponent<HitObject>().originColor = BossRenderer.color;
        UiManager.Instance.BossUI_On();

    }
}
