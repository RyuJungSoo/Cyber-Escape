using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transporter : MonoBehaviour
{
    public float Force = 5f;
    public bool isStop;
    public bool isReverse;
    private SpriteRenderer spriteRenderer;

    public float restartTimer = 0f;


    // Start is called before the first frame update
    void Start()
    {

        spriteRenderer = GetComponent<SpriteRenderer>();
        Settings();
    }

    private void Update()
    {
        if (isStop)
        {
            restartTimer += Time.deltaTime;
            if (restartTimer >= 10)
            {
                TransporterStart();
                restartTimer = 0;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isStop || Time.timeScale == 0)
            return;

        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Obstacle"))
        {

            if (isReverse == true)
            {

                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-Force, 0), ForceMode2D.Force);
            }
            else
            {

                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(Force, 0), ForceMode2D.Force);
            }

        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (isStop)
            return;

        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Obstacle"))
        {
            
            if (isReverse == true)
            {
                
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-Force, 0), ForceMode2D.Force);
            }
            else
            {
                
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(Force, 0), ForceMode2D.Force);
            }

        }
    }

    public void Settings() // 정지, 재생, 역재생 애니메이션 세팅
    {
        if (isStop == true)
        {
            Animator[] animators = transform.GetComponentsInChildren<Animator>();
            foreach (Animator animator in animators)
            {
                animator.speed = 0;
            }
        }

        else if (isReverse == true && isStop == false)
        {
            Animator[] animators = transform.GetComponentsInChildren<Animator>();
            foreach (Animator animator in animators)
            {
                animator.speed = 1;
                animator.SetFloat("Reverse", -1);
            }
        }
        else if (isReverse == false && isStop == false)
        {
            Animator[] animators = transform.GetComponentsInChildren<Animator>();
            foreach (Animator animator in animators)
            {
                animator.speed = 1;
                animator.SetFloat("Reverse", 1);
            }

        }

        else if (isStop == false)
        {
            Animator[] animators = transform.GetComponentsInChildren<Animator>();
            foreach (Animator animator in animators)
            {
                animator.speed = 1;
            }
        }
    }

    public void TransporterStop()
    {
        Force = 0;
        isStop = true;
        Settings();
    }

    public void TransporterStart()
    {
        Force = 12;
        isStop = false;
        Settings();
    }

    public void TransporterReverse()
    {
        isReverse = !isReverse;
        Settings();
    }
}
