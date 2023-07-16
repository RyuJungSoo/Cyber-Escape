using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class RushMonster : MonoBehaviour
{
    // Start is called before the first frame update

    public float rushRange = 5f;
    public float rushSpeed = 10f;
    public float rushCoolTimer = 0f;


    [SerializeField]
    private bool isRush = false;
    private float rushTime = 0;
    private float stunTimer = 0;

    MonsterComponent monster;
    private Rigidbody2D EnemyRigid;
    private Animator animator;
    private Vector2 originPos;

    void Start()
    {
        monster = GetComponent<MonsterComponent>();
        animator = GetComponent<Animator>();
        EnemyRigid = GetComponent<Rigidbody2D>();
        originPos = EnemyRigid.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isRush && rushTime <= 0)
            rushCoolTimer += Time.deltaTime;
        
       
        RaycastHit2D[] rayHits = Physics2D.RaycastAll(transform.position, monster.direction, rushRange);
        foreach (RaycastHit2D rayHit in rayHits)
        {
            if (rayHit.collider.gameObject.CompareTag("Player") && rayHit.collider.gameObject != gameObject && !isRush && rushCoolTimer >= 3f)
            {
                isRush = true;
                animator.SetBool("isRush", true);
                rushCoolTimer = 0;
            }
        }

        if (isRush)
        {
            monster.isWalk = false;

            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
            {
                StartCoroutine("Rush");
                Debug.Log("内风凭");
            }

        }
    }

    IEnumerator Rush()
    {
        isRush = false;

        while (rushTime <= 2f)
        {
            monster.isWalk = false;

            rushTime += Time.deltaTime;
            transform.localScale.Set(monster.direction.x, 1, 1);

            EnemyRigid.MovePosition(EnemyRigid.position + monster.direction * rushSpeed * Time.fixedDeltaTime);
            RaycastHit2D[] rayHits = Physics2D.RaycastAll(transform.position, monster.direction, 1f);
            foreach (RaycastHit2D rayHit in rayHits)
            {
                if (rayHit.collider.gameObject.CompareTag("Wall"))
                {
                    animator.SetBool("isRush", false);
                    rushTime = 0;
                    isRush = false;
                    Debug.Log("胶畔内风凭");

                    StartCoroutine("Stun");
                    yield break;
                }
            }
            yield return null;
        }

        animator.SetBool("isRush", false);
        rushTime = 0;
        monster.isWalk = true;
        isRush = false;

        yield return null;
    }

    IEnumerator Stun()
    {
        while(true)
        {

            stunTimer += Time.deltaTime;
            monster.isWalk = false;

            if (stunTimer >= 2f)
            {
                monster.isWalk = true;
                stunTimer = 0;
                yield break;
            }

            yield return null;
        }
    }

}
