using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RbulletController : MonoBehaviour
{
    GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        this.Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-0.1f, 0, 0);

        if (transform.position.x < 48.0f)
        {
            gameObject.SetActive(false);
        }

        Vector2 p1 = transform.position;
        Vector2 p2 = this.Player.transform.position;

        Vector2 dir = p1 - p2;

        float d = dir.magnitude;

        float r1 = 0.3f;
        float r2 = 1.0f;

        if (d < r1 + r2)
        {
            gameObject.SetActive(false);
        }
    }
}
