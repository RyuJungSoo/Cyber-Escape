using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    GameObject Player;
    public float bulletdamage = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        this.Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, -0.1f, 0);

        if (transform.position.y < -5.75f)
        {
            // gameObject.SetActive(false);
            Destroy(gameObject);
        }

        Vector2 p1 = transform.position;
        Vector2 p2 = this.Player.transform.position;

        Vector2 dir = p1 - p2;

        float d = dir.magnitude;

        float r1 = 0.3f;
        float r2 = 1.0f;

        if (d < r1 + r2 && Player.GetComponent<PlayerController>().Hp > 0)
        {
            GameManager.Instance.PlayerDamage(bulletdamage, false);

            gameObject.SetActive(false);
        }
    }
}
