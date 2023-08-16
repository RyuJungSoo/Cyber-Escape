using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialComponent : MonoBehaviour
{
    private void Update()
    {
        if (UiManager.Instance.TutorialUI.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                UiManager.Instance.TutorialUI_Off();
                this.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
            UiManager.Instance.TutorialUI_On();
    }
}
