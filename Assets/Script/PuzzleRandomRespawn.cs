using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleRandomRespawn : MonoBehaviour
{
    public GameObject Puzzle;
    public GameObject Player;
    float currTime;   

    // Update is called once per frame
    void Update()
    {
        if (Player.transform.position.x >= 230.5f && Player.transform.position.x <= 249f) //3스테이지 진입 후
        {
            currTime += Time.deltaTime;

            if (currTime > 5)                      //
            {
                float newX = Random.Range(232f, 248f);
                float newY = -5.8f;
                GameObject puzzle = Instantiate(Puzzle);
                puzzle.transform.position = new Vector3(newX, newY, 0);

                currTime = 0;
            }
        }

    }
}
