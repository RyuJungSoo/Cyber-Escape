using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public bool IsHuman; // ������� �κ����� ����
    public float Hp = 10; // ü��
    public float MaxHp = 10; // �ִ� ü��
    public float damage = 3; // ������
    public float speed = 5; // ���ǵ�
    public float moveRange = 2; // �¿� �̵� �Ÿ�

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
            Debug.Log("�÷��̾� ������");
    }

    private void Move()
    {
        angle += Time.deltaTime * speed * direction;

        EnemyRigid.MovePosition(originPos + moveRange * new Vector2(Mathf.Sin(angle), 0)); // transform���� �̵���Ű�� ���� �հ� �� �� �ֱ� ������ MovePosition�� �����.


        if (angle > 90 * Mathf.Deg2Rad || angle < 0)
        {
            direction = direction * -1;
            transform.localScale = new Vector2(-1 * transform.localScale.x, transform.localScale.y);
        }

    }


}