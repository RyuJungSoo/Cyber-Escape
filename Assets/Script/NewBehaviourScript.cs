using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public bool IsHuman; // 사람인지 로봇인지 유무
    public float Hp = 10; // 체력
    public float MaxHp = 10; // 최대 체력
    public float damage = 3; // 데미지
    public float speed = 5; // 스피드
    public float moveRange = 2; // 좌우 이동 거리

    private Rigidbody2D EnemyRigid;
    private Animator animator;
    private Vector2 originPos;
    private float angle = 0;
    private int direction = 1;

    // Start is called before the first frame update
    void Start()
    {
        EnemyRigid = GetComponent<Rigidbody2D>();
        originPos = EnemyRigid.position;
        animator = GetComponent<Animator>();

        animator.SetBool("isWalk", true);
    }

    // Update is called once per frame
    void FixedUpdate()
    {


        Move();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            Debug.Log("플레이어 감지됨");
    }

    private void Move()
    {
        angle += Time.deltaTime * speed * direction;

        EnemyRigid.MovePosition(originPos + moveRange * new Vector2(Mathf.Sin(angle), 0)); // transform으로 이동시키면 벽을 뚫고 갈 수 있기 때문에 MovePosition을 사용함.


        if (angle > 90 * Mathf.Deg2Rad || angle < 0)
        {
            direction = direction * -1;
            transform.localScale = new Vector2(-1 * transform.localScale.x, transform.localScale.y);
        }

    }


}