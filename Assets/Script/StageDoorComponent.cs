using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageDoorComponent : MonoBehaviour
{
    // Start is called before the first frame update

    public bool isDoorOpen = false;
    public PuzzleCompononent puzzle;
    public float upSpeed = 2f;

    public float waitTimer = 3f;
    public bool isCheck = false;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 3f);
        foreach (Collider2D hit in hits)
        {
            if (hit.gameObject.CompareTag("Player"))
                isCheck = true;
        }


        if (puzzle.isSolved)
            isDoorOpen = true;

        if (!puzzle.isSolved && puzzle.isFailed && PoolManager.Instance.currentEnemyCnt == 0)
        {
            isDoorOpen = true;
            PoolManager.Instance.currentEnemyCnt = -1;
        }

        if (isDoorOpen && transform.position.y < 2.6f)
        {
            waitTimer -= Time.deltaTime;
            if (waitTimer <= 0)
                transform.position = new Vector3(transform.position.x, transform.position.y + upSpeed * Time.deltaTime, -2f);
        }

        if (transform.position.y >= 2.6f)
            Destroy(this);
            
    }
}
