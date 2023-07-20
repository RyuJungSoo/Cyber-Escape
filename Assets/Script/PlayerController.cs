using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    Rigidbody2D PlayerRigid;
    Animator animator;
    BoxCollider2D col2D;

    public float speed = 5.0f;
    public Vector3 direction;

    private float temp = 0.0f;

    private float jumpForce = 425.0f;

    private bool isdash;
    public float defaultTime;
    private float dashTime;

    public bool isGround = true;
    public bool isPuzzleSolving = false;

    void Start()
    {
        this.PlayerRigid = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
        this.col2D = GetComponent<BoxCollider2D>();

        direction = Vector2.zero;
    }

    private float curTime;
    public float coolTime = 0.5f;
    public Transform pos;
    public Vector2 boxSize;

    // Update is called once per frame
    void Update()
    {
        if (isPuzzleSolving) return;

        if (Input.GetKeyDown(KeyCode.Z)) // 공격
        {
            animator.SetTrigger("atk");
        }                                                                         

        float key = 0.0f; // 좌우 이동 방향
        if (Input.GetKey(KeyCode.RightArrow)) key = 1.0f;
        if (Input.GetKey(KeyCode.LeftArrow)) key = -1.0f;

        direction.x = Input.GetAxisRaw("Horizontal"); // 왼쪽부터 -1 0 1
        

        RaycastHit2D raycastHit = Physics2D.BoxCast(col2D.bounds.center, col2D.bounds.size, 0f, Vector2.down, 0.02f, LayerMask.GetMask("Ground"));
        if (raycastHit.collider != null)
            isGround = true;
        else 
            isGround = false;
        
        if (Input.GetKeyDown(KeyCode.Space) && isGround) // 점프
        {
            this.PlayerRigid.AddForce(transform.up * this.jumpForce);
            temp = direction.x;
        }

        if (isGround)
        {
            transform.position += direction * speed * Time.deltaTime; // 이동
        }
        else
        {
            direction.x = temp;
            transform.position += direction * speed * Time.deltaTime;
        }


        if ((key != 0.0f) && isGround) // 움직이는 방향에 맞춰 반전
        {
            transform.localScale = new Vector3(key, 1, 1);
        }

        if (key != 0.0f) // 걷기 애니메이션 설정
        {
            if(dashTime != defaultTime)
                animator.SetBool("isWalk", true);
        }
        else
        {
            animator.SetBool("isWalk", false);
        }


        // 대쉬 (이동 중에 F키)
        if (Input.GetKeyDown(KeyCode.F) && (key != 0.0f) && isGround)
        {
            isdash = true;
        }

        if (!isdash)
        {
            this.gameObject.layer = 0;
            animator.SetBool("isDash", false);
        }

        if (dashTime <= 0)
        {
            if (isdash)
            {
                dashTime = defaultTime;
                isdash = false;
            }
        }
        else
        {
            dashTime -= Time.deltaTime;
            this.gameObject.layer = 7;
            animator.SetBool("isDash", true);
            transform.Translate(key * 0.1f, 0, 0);
        }
    }

    public void Attack()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.tag == "Monster")
            {
                Debug.Log("성공!");
                collider.gameObject.GetComponent<HitObject>().ChangeColor();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pos.position, boxSize);
    }
}
