using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitObject : MonoBehaviour
{
    // Start is called before the first frame update

    SpriteRenderer renderer;
    public Color originColor;
    public bool isChange = false;
    public bool isPlayer = false;
    public bool isFadeOut = false;
    float changeTimer = 0;
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        originColor = renderer.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (isChange)
        {
            changeTimer += Time.deltaTime;
            if (changeTimer >= 0.5f)
            {
                renderer.color = originColor;
                isChange = false;
                changeTimer = 0;

                if (gameObject.CompareTag("Dummy"))
                {
                    gameObject.GetComponent<DummyController>().animator.SetBool("isAttacked", false);
                }
            }
            
        }
    }

    public void ChangeColor(bool isRed)
    {
        if (isRed)
            renderer.color = Color.red;
        else
            renderer.color = new Color(1, 1, 1, 0.5f);
        isChange = true;
    }

    public void AnimationStart()
    {
        gameObject.GetComponent<DummyController>().animator.SetBool("isAttacked", true);
    }

    public void FadeOutStart()
    {
        if (!isFadeOut)
        {
            isFadeOut = true;
            StartCoroutine("FadeOut");
        }
    }

    IEnumerator FadeOut()
    {
        while (renderer.color.a > 0f)
        {
            renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, renderer.color.a - Time.deltaTime);
            yield return null;
        }

        gameObject.SetActive(false);
        if (isPlayer == true)
        {
            GameManager.Instance.Respawn();
        }
        else
            PoolManager.Instance.currentEnemyCnt--;

    }


}
