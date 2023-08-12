using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transporter : MonoBehaviour
{
    public float Force = 5f;
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

    private void Settings() // 犁积, 开犁积 局聪皋捞记 技泼
    {
        if (isReverse == true)
        {
            Animator[] animators = transform.GetComponentsInChildren<Animator>();
            foreach (Animator animator in animators)
            {
                animator.SetFloat("Reverse", -1);
            }
        }
        else
        {
            Animator[] animators = transform.GetComponentsInChildren<Animator>();
            foreach (Animator animator in animators)
            {
                animator.SetFloat("Reverse", 1);
            }

        }
    }
}
