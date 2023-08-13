using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class BossComponent : MonoBehaviour
{
    // Start is called before the first frame update

    int attackPattern = 0;
    int direction = 1;
    public int rushCnt = 0;

    bool isJump = false;
    bool isRush = false;
    bool firstSetting = false;
    bool isReady = true;

    float jumpTimer = 0f;
    float rushTimer = 0f;
    float attackTimer = 0f;
    public float rushSpeed = 5f;

    Animator animator;

    GameObject player;

    Vector3 orginPos;

    void Start()
    {
        animator = GetComponent<Animator>();
        orginPos = transform.position;
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (isReady)
        {
            attackTimer += Time.deltaTime;
        }

        if (attackTimer >= 2f)
        {
            // attackPattern = Random.Range(1, 2);
            attackPattern = 1;
            attackTimer = 0;
            isReady = false;
        }

        if (attackPattern == 1)
        {
            animator.SetBool("isRush", true);
            if (!firstSetting)
            {
                direction = transform.position.x > player.transform.position.x ? -1 : 1;
                firstSetting = true;
            }
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
            {
                isRush = true;
            }
        }

        if (isRush)
        {
            transform.position += Vector3.right * direction * rushSpeed * Time.deltaTime;
            transform.localScale = new Vector3(direction * -1, 1, 1);

            if (direction == -1 && transform.position.x < player.transform.position.x - 5)
            {
                rushCnt++;
                direction = 1;
            }

            if (direction == 1 && transform.position.x > player.transform.position.x + 5)
            {
                rushCnt++;
                direction = -1;
            }

            if (Mathf.Abs(transform.position.x - orginPos.x) > 10)
                direction *= -1;

            if (rushCnt == 4)
            {
                isRush = false;
                attackPattern = 0;
                isReady = true;
                animator.SetBool("isRush", false);
                rushCnt = 0;
            }
        }

        else
        {
            direction = transform.position.x > player.transform.position.x ? -1 : 1;
            transform.localScale = new Vector3(direction * -1, 1, 1);
        }
    }
}
