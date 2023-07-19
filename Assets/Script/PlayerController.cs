using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    Rigidbody2D PlayerRigid;
    Animator animator;

    private float jumpForce = 500.0f;

    private float defaultForce;
    private float defaultmaxSpeed;

    private float walkForce = 30.0f;
    private float maxWalkSpeed = 3.0f;

    private bool isdash;
    public float defaultTime;
    private float dashTime;

    public bool isGround = true;

    BoxCollider2D col2D;

    void Start()
    {
        this.PlayerRigid = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
        this.col2D = GetComponent<BoxCollider2D>();

        defaultForce = walkForce;
        defaultmaxSpeed = maxWalkSpeed;
    }

    private float curTime;
    public float coolTime = 0.5f;
    public Transform pos;
    public Vector2 boxSize;

    // Update is called once per frame
    void Update()
    {
        if (curTime <= 0) // ����
        {
            if (Input.GetKey(KeyCode.Z))
            {
                Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
                foreach(Collider2D collider in collider2Ds)
                {
                    if (collider.tag == "Monster")
                    {
                        Debug.Log("����!");
                        // collider.GetComponent<MonsterComponent>().TakeDamage(1.0f);
                    }
                }

                animator.SetTrigger("atk");
                curTime = coolTime;
            }
        }
        else
        {
            curTime -= Time.deltaTime;
        }

        RaycastHit2D raycastHit = Physics2D.BoxCast(col2D.bounds.center, col2D.bounds.size, 0f, Vector2.down, 0.02f, LayerMask.GetMask("Ground"));
        if (raycastHit.collider != null)
            isGround = true;
        else 
            isGround = false;

        if (Input.GetKeyDown(KeyCode.Space) && isGround) // ����
        {
            this.PlayerRigid.AddForce(transform.up * this.jumpForce);
        }

        float key = 0.0f; // �¿� �̵�
        if (Input.GetKey(KeyCode.RightArrow)) key = 1.0f;
        if (Input.GetKey(KeyCode.LeftArrow)) key = -1.0f;

        float speedx = Mathf.Abs(this.PlayerRigid.velocity.x); // �÷��̾� �ӵ�

        if (speedx < defaultmaxSpeed) // ���ǵ� ����
        {
            this.PlayerRigid.AddForce(transform.right * key * this.defaultForce);
        }

        if ((key != 0.0f) && isGround) // �����̴� ���⿡ ���� ����
        {
            transform.localScale = new Vector3(key, 1, 1);
        }

        if (key != 0.0f) // �ȱ� �ִϸ��̼� ����
        {
            animator.SetBool("isWalk", true);
        }
        else
        {
            animator.SetBool("isWalk", false);
        }

        // �뽬
        if (Input.GetKeyDown(KeyCode.F) && isGround)
        {
            isdash = true;
        }

        if (!isdash)
        {
            this.gameObject.layer = 0;
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
            transform.Translate(key * 0.125f, 0, 0);
        }
    }    

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pos.position, boxSize);
    }
}
