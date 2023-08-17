using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushObstacle : MonoBehaviour
{
    // Start is called before the first frame update


    float childTimer = 0;
    int index = 0;
    bool isActive = false;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        childTimer += Time.deltaTime;

        if (childTimer >= 2.0f)
        {
            int randomInt = Random.Range(0, 2);
            for (int i = 0; i < 6; i++)
            {
                    if (i % 2 == randomInt)
                    {
                        transform.GetChild(i).gameObject.GetComponent<RushObstacleFadeOut>().fadeOut = true;
                    }

                isActive = true;
            }

            childTimer = 0;
        }

        if (isActive && childTimer >= 1)
        {
            for (int i =0; i < 6; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }

            isActive = false;

        }
    }
}
