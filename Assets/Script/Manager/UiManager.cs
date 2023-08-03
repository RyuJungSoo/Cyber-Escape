using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance = null; // 어디서든 접근할 수 있도록 인스턴스 선언
    public Image HpBar;
    public Text HpText;

    private void Awake()
    {
        if (Instance) // 이미 Instance에 할당된게 있으면 중복되는 걸 지우기
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
        
    }

    public void HpUI_Update()
    {
        float Player_MaxHp = GameManager.Instance.GetPlayerMaxHp();
        float Player_Hp = GameManager.Instance.GetPlayerHp();


        HpText.text = Player_Hp.ToString();
        HpBar.fillAmount = Player_Hp / Player_MaxHp;
    }
}
