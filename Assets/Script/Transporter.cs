using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transporter : MonoBehaviour
{
    public float Force = 5f;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        
       
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Obstacle"))
        {
            
            if (spriteRenderer.flipX == true)
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-Force, 0),ForceMode2D.Force);
            else
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(Force, 0), ForceMode2D.Force);

        }
    }
}
