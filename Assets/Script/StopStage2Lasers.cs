using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopStage2Lasers : MonoBehaviour
{
    public GameObject PuzzleUI;

    public GameObject[] BlinkLaser = new GameObject[3];
    public GameObject Stage2BlinkLaserController;

    // Start is called before the first frame update
    void Start()
    {
        PuzzleUI = GameObject.Find("DoorButton_Laser").GetComponent<DoorButtonController>().PuzzleUI;
    }

    // Update is called once per frame
    void Update()
    {
        if (PuzzleUI.transform.GetChild(0).GetComponent<PuzzleCompononent>().isSolved == true &&
            PuzzleUI.transform.GetChild(0).GetComponent<PuzzleCompononent>().isFailed == false)
        {
            for (int i = 0; i < 3; i++)
            {
                BlinkLaser[i].SetActive(false);
            }
            Stage2BlinkLaserController.SetActive(false);
        }   
    }
}
