using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    Rigidbody2D PlayerRigid;
    Animator animator;
    public float speed = 3f;
    public float jumpPower = 3f;

    public bool isJump = false;
    public bool isGround = true;


    Vector2 movement = new Vector2();

    BoxCollider2D col2D;

    void Start()
    {
        PlayerRigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        col2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(col2D.bounds.center, col2D.bounds.size, 0f, Vector2.down, 0.02f, LayerMask.GetMask("Ground"));
        if (raycastHit.collider != null)
            isGround = true;
        else 
            isGround = false;

        movement.x = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && isGround)
            isJump = true;

    }

    void Move()
    {
 
        if (Mathf.Abs(movement.x) > 0)
        {
            animator.SetBool("isWalk", true);
            transform.localScale = new Vector3(movement.x, transform.localScale.y);
        }
        else
            animator.SetBool("isWalk", false);

       PlayerRigid.velocity = new Vector2(speed * movement.x, PlayerRigid.velocity.y);
    }

    void Jump()
    {
        if (!isJump)
            return;

      PlayerRigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
      isJump = false;
          
    }

    private void FixedUpdate()
    {
        Move();
        Jump();

    }
}
