using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null; // ��𼭵� ������ �� �ֵ��� �ν��Ͻ� ����
    [SerializeField]private int Stage = 0;
    public Vector2[] Stage_Pos;
    public GameObject Player;
    public bool isOver;
    private PlayerController playerController;


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
        Player.transform.position = Stage_Pos[Stage];
    }

    public void PlayerDamage(float Damage) 
    {
        playerController.Hp -= Damage;
        if (playerController.Hp <= 0)
        {
            playerController.Hp = 0;
        }

        UiManager.Instance.HpUI_Update();

        if (playerController.Hp <= 0)
        {
            isOver = true;
            Debug.Log("Game Over");
        }
    }
}
