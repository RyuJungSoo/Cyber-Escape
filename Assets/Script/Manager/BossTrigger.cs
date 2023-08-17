using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.ChangeBGM(2);
            GameManager.Instance.Boss_On();
            this.gameObject.SetActive(false);
        }
    }
}
