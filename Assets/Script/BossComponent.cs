using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading;
using System.Timers;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BossComponent : MonoBehaviour
{
    // Start is called before the first frame update

    int attackPattern = 0;
    int direction = 1;
    public int rushCnt = 0;
    public int jumpCnt = 0;


    bool isJump = false;
    bool isRush = false;
    bool firstSetting = false;
    bool isReady = true;

    bool changeBeltDirection = false;

    float jumpTimer = 0f;
    float rushTimer = 0f;
    float attackTimer = 0f;
    float jumpAngle = 0f;
    float gravity = 0f;
    
    public float rushSpeed = 5f;
    public float rushDamage = 5f;

    public float jumpSpeed = 5f;

    public GameObject rushObstacles;
    public GameObject transporters;
    Animator animator;

    GameObject player;

    Vector3 orginPos;
    Vector3 jumpStartPos;
    Vector3 targetPos;


    void Start()
    {

        animator = GetComponent<Animator>();
        orginPos = transform.position;
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x < 230) return;

        if (isReady)
        {
            attackTimer += Time.deltaTime;
        }

        if (attackTimer < 4f && changeBeltDirection)
        {
            transporters.GetComponent<Transporter>().isReverse = !transporters.GetComponent<Transporter>().isReverse;
            changeBeltDirection = false;
        }

        if (attackTimer >= 4f)
        {
            attackPattern = UnityEngine.Random.Range(1, 3);
            
            attackTimer = 0;
            isReady = false;
        }

        if (attackPattern == 1)
        {
            animator.SetBool("isRush", true);
            if (!firstSetting)
            {
                direction = transform.position.x > player.transform.position.x ? -1 : 1;
                rushObstacles.SetActive(true);
                jumpStartPos = transform.position;
                targetPos = player.transform.position ;
                firstSetting = true;
            }
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
            {
                isRush = true;
            }
        }

        if (attackPattern == 2)
        {
            animator.SetBool("isRush", true);

            if (!firstSetting)
            {
                direction = transform.position.x > player.transform.position.x ? -1 : 1;
                targetPos = new Vector3(player.transform.position.x,transform.position.y,transform.position.z);
                jumpAngle = Mathf.Rad2Deg * Mathf.Acos(targetPos.x);
                jumpStartPos = transform.position;
                firstSetting = true;
            }

            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
            {
                animator.SetBool("isJump", true);
                Debug.Log("¤¾¤¾");
                isJump = true;
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

            if (Mathf.Abs(transform.position.x - orginPos.x) > 9)
                direction *= -1;

            if (rushCnt == 3)
            {
                isRush = false;
                attackPattern = 0;
                isReady = true;
                animator.SetBool("isRush", false);
                rushObstacles.SetActive(false);
                firstSetting = false;
                changeBeltDirection = true;

                rushCnt = 0;
            }
        }

        else
        {
            direction = transform.position.x > player.transform.position.x ? -1 : 1;
            transform.localScale = new Vector3(direction * -1, 1, 1);
        }

        if (isJump)
        {

            float x0 = jumpStartPos.x;
            float x1 = targetPos.x;
            float distance = x1 - x0;
            float nextX = Mathf.MoveTowards(transform.position.x, x1, jumpSpeed * Time.deltaTime);
            float baseY = Mathf.Lerp(jumpStartPos.y, targetPos.y, (nextX - x0) / distance);
            float arc = 4 * (nextX - x0) * (nextX - x1) / (-0.25f * distance * distance);
            Vector3 nextPosition = new Vector3(nextX, baseY + arc, transform.position.z);

            transform.position = nextPosition;

            if (nextPosition == targetPos)
            {
                isJump = false;
                firstSetting = false;
                jumpCnt++;

                if (jumpCnt == 3)
                {
                    attackPattern = 0;
                    isReady = true;
                    changeBeltDirection = true;
                    jumpCnt = 0;
                }    
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.gameObject.layer != 8)
                GameManager.Instance.PlayerDamage(rushDamage, true);
        }
    }

  
}
