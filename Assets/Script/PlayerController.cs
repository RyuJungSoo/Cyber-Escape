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
    AudioSource audioSource;

    public AudioClip[] audioClips;
    public float speed = 5.0f;
    public Vector3 direction;

    private float temp = 0.0f;

    private float jumpForce = 425.0f;

    private bool isdash;
    public float defaultTime;
    private float dashTime;

    public float dashCooldown = 2.0f;
    private float dashTimer = 0.0f;

    public Transform pos;
    public Vector2 boxSize;

    public bool isGround = true;
    public bool isPuzzleSolving = false;


    void Start()
    {
        this.PlayerRigid = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
        this.col2D = GetComponent<BoxCollider2D>();
        this.audioSource = GetComponent<AudioSource>();
        direction = Vector2.zero;
    }    

    // Update is called once per frame
    void Update()
    {
        if (isPuzzleSolving) return;


        if (Input.GetKeyDown(KeyCode.Z)) // ����
        {
            animator.SetTrigger("atk");
            PlayEffect(3);
        }                                                                         


        float key = 0.0f; // �¿� �̵� ����
        if (Input.GetKey(KeyCode.RightArrow))
        {
            key = 1.0f;
            
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        { 
            key = -1.0f;
            
        }

        direction.x = Input.GetAxisRaw("Horizontal"); // ���ʺ��� -1 0 1
        if (direction.x != 0)
        {
            audioSource.loop = true;
            PlayEffect(0);

        }
        else
            audioSource.loop = false;
            

        RaycastHit2D raycastHit = Physics2D.BoxCast(col2D.bounds.center, col2D.bounds.size, 0f, Vector2.down, 0.02f, LayerMask.GetMask("Ground"));
        if (raycastHit.collider != null)
            isGround = true;
        else 
            isGround = false;

        if (Input.GetKeyDown(KeyCode.Space) && isGround) // ����
        {
            this.PlayerRigid.AddForce(transform.up * this.jumpForce);
            temp = direction.x;
            PlayEffect(2);
        }

        
        if (isGround)
        {
            transform.position += direction * speed * Time.deltaTime; // �̵�
        }
        else
        {
            direction.x = temp;
            transform.position += direction * speed * Time.deltaTime;
        }
        

        if ((key != 0.0f) && isGround) // �����̴� ���⿡ ���� ����
        {
            transform.localScale = new Vector3(key, 1, 1);
        }

        if (key != 0.0f) // �ȱ� �ִϸ��̼� ����
        {
            if(dashTime != defaultTime)
                animator.SetBool("isWalk", true);
        }
        else
        {
            animator.SetBool("isWalk", false);
        }


        // �뽬 (�̵� �߿� FŰ) (��Ÿ�� 2��)                                      
        if (Input.GetKeyDown(KeyCode.F) && (key != 0.0f) && isGround && dashTimer <= 0.0f)
        {
            isdash = true;
            dashTimer = dashCooldown;
            PlayEffect(1);
        }

        if (dashTimer > 0.0f)
        {
            dashTimer -= Time.deltaTime;
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
                Debug.Log("����!");
                collider.gameObject.GetComponent<HitObject>().ChangeColor();
            }
        }
    }

    public void PlayEffect(int index)
    {
        if (index != 0)
            audioSource.PlayOneShot(audioClips[index]);
        else
        {
            if (!audioSource.isPlaying)
            {

                audioSource.Play();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pos.position, boxSize);
    }
}
