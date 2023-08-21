using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleRandomRespawn : MonoBehaviour
{
    public GameObject Puzzle;
    public GameObject Player;

    public Transporter transporter;

    float currTime;
    float transporterStopTimer = 0;

    public bool isSolve = false;

    // Update is called once per frame
    void Update()
    {
        if (Player.transform.position.x >= 230.5f && Player.transform.position.x <= 249f) //3스테이지 진입 후
        {
            if(!GameManager.Instance.isBossPuzzleUION && transporter.Force > 0)
                currTime += Time.deltaTime;

            if (transporterStopTimer > 10f)
            {
                transporterStopTimer = 0;
                transporter.TransporterStart();
            }

            if (currTime > 10)                      //
            {
                float newX = Random.Range(232f, 248f);
                float newY = -5.8f;

                Puzzle.transform.position = new Vector3(newX, newY, -1);
                Puzzle.GetComponent<DoorButtonController>().Reset();
                Puzzle.SetActive(true);
                currTime = 0;
            }
        }

    }
}
