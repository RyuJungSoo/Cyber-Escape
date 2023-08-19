using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorTutorialComponent : MonoBehaviour
{
    private void Update()
    {
        if (UiManager.Instance.ElevatorTutorialUI.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                UiManager.Instance.ElevatorTutorialUI_Off();
                this.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            UiManager.Instance.ElevatorTutorialUI_On();
    }
}
