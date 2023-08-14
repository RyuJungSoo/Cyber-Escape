using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    Rigidbody2D PlayerRigid;
    Animator animator;
    BoxCollider2D col2D;
    AudioSource audioSource;
    SpriteRenderer spriteRenderer;

    public AudioClip[] audioClips;
    public float Hp = 100;
    public float MaxHp = 100;
    public float speed = 5.0f;
    public Vector3 direction;

    private float temp = 0.0f;

    private float jumpForce = 425.0f;

    private bool isdash;
    public float defaultTime;
    public float damage = 5f;

    public Image skill_UI;
    private float dashTime;

    public float dashCooldown = 2.0f;
    private float dashTimer = 0.0f;

    public Transform pos;
    public Vector2 boxSize;

    public bool isGround = true;
    public bool isPuzzleSolving = false;
    public bool isDead = false;

    void Start()
    {
        this.PlayerRigid = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
        this.col2D = GetComponent<BoxCollider2D>();
        this.audioSource = GetComponent<AudioSource>();
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        direction = Vector2.zero;
    }    

    // Update is called once per frame
    void Update()
    {
        if (isPuzzleSolving) return;


        if (Input.GetKeyDown(KeyCode.Z)) // 공격
        {
            animator.SetTrigger("atk");
            PlayEffect(3);
        }                                                                         


        float key = 0.0f; // 좌우 이동 방향
        if (Input.GetKey(KeyCode.RightArrow))
        {
            key = 1.0f;
            
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        { 
            key = -1.0f;
            
        }

        direction.x = Input.GetAxisRaw("Horizontal"); // 왼쪽부터 -1 0 1
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

        if (Input.GetKeyDown(KeyCode.Space) && isGround) // 점프
        {
            this.PlayerRigid.AddForce(transform.up * this.jumpForce);
            temp = direction.x;
            PlayEffect(2);
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

        if (transform.position.x >= 272f)
        {
            SceneManager.LoadScene("EndingScene");
        }

        // 대쉬 (이동 중에 F키) (쿨타임 2초)                                      
        if (Input.GetKeyDown(KeyCode.F) && (key != 0.0f) && isGround && dashTimer <= 0.0f)
        {
            isdash = true;
            dashTimer = dashCooldown;
            StartCoroutine(CoolTime());
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
            this.gameObject.layer = 8;
            animator.SetBool("isDash", true);
            transform.Translate(key * 0.1f, 0, 0);
        }
    }

    public void Attack()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.CompareTag("Monster")  || collider.CompareTag("Dummy"))
            {
                if (collider.gameObject.GetComponent<HitObject>().isFadeOut) return;

                Debug.Log("성공!");
                collider.gameObject.GetComponent<HitObject>().ChangeColor(true);
                collider.gameObject.GetComponent<MonsterComponent>().TakeDamage(damage);
            }

            if (collider.CompareTag("Dummy"))
            {
                collider.gameObject.GetComponent<HitObject>().AnimationStart();
            }
        }
    }

    public void PlayEffect(int index)
    {
        if (UiManager.Instance.PauseUI.active == true)
            return;
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

    void OnCollisionEnter2D (Collision2D collision)
    {
        if (collision.gameObject.tag == "Laser")
        {
            OnDamaged(collision.transform.position, true,3);
        }
    }

    

    public void OnDamaged(Vector2 targetPos, bool isPushed, float power)
    {
        Debug.Log("장애물에 닿았습니다!");

        this.gameObject.layer = 9;

        GameManager.Instance.PlayerDamage(5f, false);
        if (isPushed)
        {
            Debug.Log("밀었습니다!");

            int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
            if (dirc == 1)
            {
                this.PlayerRigid.AddForce(new Vector2(dirc, 1) * power, ForceMode2D.Impulse);
            }
            else
            {
                this.PlayerRigid.AddForce(new Vector2(dirc * 2, 1) * power, ForceMode2D.Impulse);
            }
        }

        Invoke("OffDamaged", 0.5f);
    }

    void OffDamaged()
    {
        this.gameObject.layer = 0;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pos.position, boxSize);
    }

    IEnumerator CoolTime()
    {
        print("쿨타임 코루틴 실행");

        while (dashTimer >= 0)
        {

            skill_UI.fillAmount = (1.0f * (dashCooldown - dashTimer) / dashCooldown);
            yield return new WaitForFixedUpdate();
        }
        print("쿨타임 코루틴 완료");
        skill_UI.fillAmount = 0;
    }

    public void HitSoundPlay()
    {
        audioSource.PlayOneShot(audioClips[4]);
    }
}
