using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumSpace;
using Unity.VisualScripting.FullSerializer.Internal;

public class MonsterComponent : MonoBehaviour
{
    public bool IsHuman; // 사람인지 로봇인지 유무
    public float Hp = 10; // 체력
    public float MaxHp = 10; // 최대 체력
    public float damage = 3; // 데미지
    public float speed = 5; // 스피드
    public float moveRange = 2; // 좌우 이동 거리
    public bool isWalk = true;
    public bool isBoss; // 보스 유무

    public Vector2 direction = new Vector2(-1, 0);
    public Vector3 originPos;

    public MonsterType type;


    private Rigidbody2D EnemyRigid;
    private Animator animator;
    private float angle = 0;
    private float attackTimer = 0f;
    private bool isDead = false;

    GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        EnemyRigid = GetComponent<Rigidbody2D>();
        //originPos = EnemyRigid.position;
        animator = GetComponent<Animator>();
        animator.SetBool("isWalk", true);

        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (type == MonsterType.DUMMY) return;
        if (Hp <= 0)
        {
            isDead = true;
            animator.speed = 0.0f;
        }
        if (isWalk && !isDead)
            Move();

        attackTimer += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && type != MonsterType.DUMMY)
        {
            if (collision.gameObject.GetComponent<PlayerController>().isDead == true)
                return;

            if (collision.gameObject.GetComponent<PlayerController>().Hp <= 0)
                return;


            GameManager.Instance.PlayerDamage(damage , true);
        }

        /*if (collision.gameObject.CompareTag("Wall"))
        {
            direction *= -1;
            transform.localScale = new Vector3(direction.x, transform.localScale.y, transform.localScale.z);
        }*/
    }

    private void Move()
    {
        animator.SetBool("isWalk", true);
        if (type == MonsterType.ATTACKROBOT)
            transform.localScale=  new Vector3(direction.x , transform.localScale.y, transform.localScale.z);
        else if (type == MonsterType.ROADROBOT)
            transform.localScale = new Vector3(direction.x * 0.75f, transform.localScale.y, transform.localScale.z);

        attackTimer += Time.deltaTime;

        if (Mathf.Abs(EnemyRigid.position.x - originPos.x) > moveRange) // moveRange 이상의 거리를 이동한 경우, 방향 바꾸기
        {
            direction *= -1;
            transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y, transform.localScale.z); // 크기가 1,1,1인 경우에는 상관없지만 스케일이 큰 경우에는 강제적으로 x축 크기가 1로 고정되버리는 상황이 발생해서 수정
        }

        EnemyRigid.MovePosition(EnemyRigid.position + direction * speed * Time.fixedDeltaTime); // transform으로 이동시키면 벽을 뚫고 갈 수 있기 때문에 MovePosition을 사용함.
        transform.position = new Vector3(transform.position.x, transform.position.y, -1);


        if (attackTimer >= 2f && type == MonsterType.ATTACKROBOT)
        {

            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 0.8f);
            foreach (Collider2D hit in hits)
            {
                if (hit.gameObject.CompareTag("Player"))
                {
                    isWalk = false;
                    animator.SetBool("isAttack", true);
                    animator.SetBool("isWalk", false);
                }
            }

            attackTimer = 0f;
        }
        else if (attackTimer < 2f && type == MonsterType.ATTACKROBOT)
            isWalk = true;
    }

    void Attack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 0.5f);
        foreach (Collider2D hit in hits)
        {
            if (hit.gameObject.CompareTag("Player"))
            {
                if (hit.gameObject.GetComponent<PlayerController>().isDead == true)
                    return;

                if (hit.gameObject.GetComponent<PlayerController>().Hp <= 0) return;

                if (hit.gameObject.transform.position.x > transform.position.x)
                    transform.localScale = new Vector2(1, transform.localScale.y);
                else
                    transform.localScale = new Vector2(-1, transform.localScale.y);

                GameManager.Instance.PlayerDamage(damage , true);
                Debug.Log("때렸습니다.");
            }
        }

        animator.SetBool("isAttack", false);
        isWalk = true;
        direction.Set(transform.localScale.x,0);
    }

    void Rush()
    {
        
    }

    public void TakeDamage(float PlayerDamage)
    {
        Hp -= PlayerDamage;
        if (isBoss)
        {
            
            if (UiManager.Instance.BossHpBar.IsActive())
            {
                UiManager.Instance.BossUI_Update();
            }
        }

        if (Hp <= 0)
        {
            //this.gameObject.SetActive(false);
           gameObject.GetComponent<HitObject>().FadeOutStart();
        }
    }

    public void Reset()
    {
        Hp = MaxHp;
        isDead = false;
        isWalk = true;
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
        GetComponent<Animator>().SetBool("isWalk", true);
        animator.speed = 1f;
        attackTimer = 0;
        GetComponent<HitObject>().isFadeOut = false;
    }
}
