using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitObject : MonoBehaviour
{
    // Start is called before the first frame update

    SpriteRenderer renderer;
    Color originColor;
    bool isChange = false;
    bool isFadeOut = false;
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

    public void ChangeColor()
    {        
        renderer.color = Color.red;
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
    }
  
}
