using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushObstacleFadeOut : MonoBehaviour
{
    // Start is called before the first frame update

    SpriteRenderer renderer;
    BoxCollider2D collider2D;
    GameObject player;
    public bool fadeOut = false;

    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();    
        collider2D = GetComponent<BoxCollider2D>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isClear)
            fadeOut = true;

        if (fadeOut)
            renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, renderer.color.a - Time.deltaTime / 2);

        if (renderer.color.a < 0 && !GameManager.Instance.isClear)
        {
            renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 1);
            fadeOut = false;
            gameObject.SetActive(false);
        }

        if (player.transform.position.y < -4.4)
            collider2D.isTrigger = true;
        else
            collider2D.isTrigger = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collider2D.isTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collider2D.isTrigger = false;
        }
    }
}
