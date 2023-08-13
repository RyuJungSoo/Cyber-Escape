using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushObstacleFadeOut : MonoBehaviour
{
    // Start is called before the first frame update

    SpriteRenderer renderer;

    public bool fadeOut = false;

    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();    
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeOut)
        renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, renderer.color.a - Time.deltaTime / 2);

        if (renderer.color.a < 0)
        {
            renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 1);
            fadeOut = false;
            gameObject.SetActive(false);
        }
    }
}
