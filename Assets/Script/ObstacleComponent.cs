using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumSpace;

public class ObstacleComponent : MonoBehaviour
{
    // Start is called before the first frame update

    public float damage = 5f;
    public bool isNotAttacking = false;
    public ObstacleType type;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isNotAttacking)
        {
            collision.gameObject.GetComponent<PlayerController>().OnDamaged(transform.position,true,3);
            GameManager.Instance.PlayerDamage(damage);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isNotAttacking)
        {
            collision.gameObject.GetComponent<PlayerController>().OnDamaged(transform.position, true,3);
            GameManager.Instance.PlayerDamage(damage);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && type == ObstacleType.PRESSTRAP)
        {
            float y = collision.gameObject.transform.localScale.y < 0.3f ?  0.3f : collision.gameObject.transform.localScale.y;
            collision.gameObject.transform.localScale = new Vector3(1,  y-Time.deltaTime * 2f, 1);
            GameManager.Instance.PlayerDamage(damage);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && type == ObstacleType.PRESSTRAP)
        {
            collision.gameObject.transform.localScale = new Vector3(1, 1, 1);
            GameManager.Instance.PlayerDamage(damage);
        }
    }

}
