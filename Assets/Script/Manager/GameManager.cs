using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null; // 어디서든 접근할 수 있도록 인스턴스 선언
    [SerializeField]private int Stage = 0;
    public Vector2[] Stage_Pos;
    public GameObject Player;
    public bool isOver;
    private PlayerController playerController;


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
