using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transporter : MonoBehaviour
{
    public float Force = 5f;
    public bool isStop;
    public bool isReverse;
    private SpriteRenderer spriteRenderer;


    // Start is called before the first frame update
    void Start()
    {

        spriteRenderer = GetComponent<SpriteRenderer>();
        Settings();
    }

    private void OnCollisionEnter2D(Collision2D collision)
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

    private void Settings() // ����, ���, ����� �ִϸ��̼� ����
    {
        if (isStop == true)
        {
            Animator[] animators = transform.GetComponentsInChildren<Animator>();
            foreach (Animator animator in animators)
            {
                animator.speed = 0;
            }
        }

        else if (isReverse == true)
        {
            Animator[] animators = transform.GetComponentsInChildren<Animator>();
            foreach (Animator animator in animators)
            {
                animator.SetFloat("Reverse", -1);
            }
        }
        else if (isReverse == false)
        {
            Animator[] animators = transform.GetComponentsInChildren<Animator>();
            foreach (Animator animator in animators)
            {
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
}
