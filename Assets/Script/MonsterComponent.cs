using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumSpace;
public class MonsterComponent : MonoBehaviour
{
    public bool IsHuman; // ������� �κ����� ����
    public float Hp = 10; // ü��
    public float MaxHp = 10; // �ִ� ü��
    public float damage = 3; // ������
    public float speed = 5; // ���ǵ�
    public float moveRange = 2; // �¿� �̵� �Ÿ�
    public bool isWalk = true;
    public Vector2 direction = new Vector2(-1, 0);

    [SerializeField]
    MonsterType type;


    private Rigidbody2D EnemyRigid;
    private Animator animator;
    private Vector2 originPos;
    private float angle = 0;
    private float attackTimer = 0f;

    GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        EnemyRigid = GetComponent<Rigidbody2D>();
        originPos = EnemyRigid.position;
        animator = GetComponent<Animator>();
        animator.SetBool("isWalk", true);

        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (isWalk)
            Move();

        attackTimer += Time.deltaTime;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<HitObject>().ChangeColor();
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            direction *= -1;
            transform.localScale = new Vector3(direction.x, transform.localScale.y, transform.localScale.z);
        }
    }

    private void Move()
    {
        animator.SetBool("isWalk", true);
        
        transform.localScale=  new Vector3(direction.x, transform.localScale.y, transform.localScale.z);
        attackTimer += Time.deltaTime;

        EnemyRigid.MovePosition(EnemyRigid.position + direction * speed * Time.fixedDeltaTime); // transform���� �̵���Ű�� ���� �հ� �� �� �ֱ� ������ MovePosition�� �����.

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

                if (hit.gameObject.transform.position.x > transform.position.x)
                    transform.localScale = new Vector2(1, transform.localScale.y);
                else
                    transform.localScale = new Vector2(-1, transform.localScale.y);

                hit.gameObject.GetComponent<HitObject>().ChangeColor();
                Debug.Log("���Ƚ��ϴ�.");
            }
        }

        animator.SetBool("isAttack", false);
        isWalk = true;
        direction.Set(transform.localScale.x,0);
    }

    void Rush()
    {
        
    }
}
