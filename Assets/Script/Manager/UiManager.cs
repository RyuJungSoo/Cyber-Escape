using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance = null; // ��𼭵� ������ �� �ֵ��� �ν��Ͻ� ����
    public Image HpBar;
    public Text HpText;

    private void Awake()
    {
        if (Instance) // �̹� Instance�� �Ҵ�Ȱ� ������ �ߺ��Ǵ� �� �����
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
        
    }

    public void HpUI_Update()
    {
        float Player_MaxHp = GameManager.Instance.GetPlayerMaxHp();
        float Player_Hp = GameManager.Instance.GetPlayerHp();


        HpText.text = Player_Hp.ToString();
        HpBar.fillAmount = Player_Hp / Player_MaxHp;
    }
}
