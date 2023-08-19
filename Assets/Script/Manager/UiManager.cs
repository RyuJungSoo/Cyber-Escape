using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance = null; // 어디서든 접근할 수 있도록 인스턴스 선언
    public GameObject BossUI;
    public Image HpBar;
    public Image BossHpBar;
    public Text HpText;
    public GameObject PauseUI;
    public GameObject TutorialUI;
    public GameObject ElevatorTutorialUI;


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

    private void FixedUpdate()
    {
        
    }


    public void HpUI_Update()
    {
        float Player_MaxHp = GameManager.Instance.GetPlayerMaxHp();
        float Player_Hp = GameManager.Instance.GetPlayerHp();


        HpText.text = Player_Hp.ToString();
        HpBar.fillAmount = Player_Hp / Player_MaxHp;
    }

    public void BossUI_Update()
    {
        float Boss_MaxHp = GameManager.Instance.GetBossMaxHp();
        float Boss_Hp = GameManager.Instance.GetBossHp();

        BossHpBar.fillAmount = Boss_Hp / Boss_MaxHp;
    }

    public void BossUI_On()
    {
        BossUI.SetActive(true);
        StartCoroutine("BossUI_Setting");
    }

    public void BossUI_Off()
    {
        BossUI.SetActive(false);
    
    }

    public void PauseUI_On()
    {

        Time.timeScale = 0;
        PauseUI.SetActive(true);
    
    }

    public void PauseUI_Off()
    {

        Time.timeScale = 1;
        PauseUI.SetActive(false);
    }

    public void TutorialUI_On()
    {

        Time.timeScale = 0;
        TutorialUI.SetActive(true);

    }

    public void TutorialUI_Off()
    {

        Time.timeScale = 1;
        TutorialUI.SetActive(false);

    }

    public void ElevatorTutorialUI_On()
    {

        Time.timeScale = 0;
        ElevatorTutorialUI.SetActive(true);

    }

    public void ElevatorTutorialUI_Off()
    {

        Time.timeScale = 1;
        ElevatorTutorialUI.SetActive(false);

    }

    IEnumerator BossUI_Setting()
    {
        while (BossHpBar.fillAmount < 1)
        {
            BossHpBar.fillAmount += 0.3f*Time.unscaledDeltaTime;
            yield return null;
        }
        Time.timeScale = 1;
    }
}
