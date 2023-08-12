using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    SpriteRenderer renderer;
    public static GameManager Instance = null; // ��𼭵� ������ �� �ֵ��� �ν��Ͻ� ����
    [SerializeField]private int Stage = 0;
    public Vector2[] Stage_Pos;
    public GameObject Player;
    private PlayerController playerController;
    private HitObject hitObject;

    public float Respawn_Time = 1f;


    private void Awake()
    {
        if(Instance) // �̹� Instance�� �Ҵ�Ȱ� ������ �ߺ��Ǵ� �� �����
        {
            DestroyImmediate(this.gameObject);
            return;
        }
        Instance = this; // Instance �Ҵ��� �� �Ǿ� �ִ� ��� ���� ���� �Ҵ�
        //DontDestroyOnLoad(gameObject);

    }

    // Start is called before the first frame update
    void Start()
    {
        playerController = Player.GetComponent<PlayerController>();
        hitObject = Player.GetComponent<HitObject>();
        renderer = Player.GetComponent<SpriteRenderer>();
        UiManager.Instance.HpUI_Update();
    }

    public float GetPlayerHp()
    {
        return playerController.Hp;
    }

    public float GetPlayerMaxHp()
    {
        return playerController.MaxHp;
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
        //�ǰݽ� 0.5�� ����
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
}
