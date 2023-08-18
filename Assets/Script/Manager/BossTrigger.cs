using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Time.timeScale = 0;
            GameManager.Instance.ChangeBGM(3);
            StartCoroutine(BossStart());
        }
    }

    IEnumerator BossStart()
    {
        yield return new WaitForSecondsRealtime(4);
        GameManager.Instance.ChangeBGM(2);
        GameManager.Instance.Boss_On();
        this.gameObject.SetActive(false);
    }
}
