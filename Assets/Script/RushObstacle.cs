using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushObstacle : MonoBehaviour
{
    // Start is called before the first frame update


    bool isEven = true;
    float childTimer = 0;
    int index = 0;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        childTimer += Time.deltaTime;

        if (childTimer >= 2.0f)
        {
            for (int i = 0; i < 10; i++)
            {
                if (isEven)
                {
                    if (i % 2 == 0)
                    {
                        transform.GetChild(i).gameObject.GetComponent<RushObstacleFadeOut>().fadeOut = true;
                        Debug.Log(i);
                    }

                    if (childTimer >= 4)
                    {

                        for (int j = 0; j < 10; j++)
                        {
                             transform.GetChild(j).gameObject.SetActive(true);
                        }

                        childTimer = 0;
                        isEven = !isEven;
                    }
                }

                else
                {
                    if (i % 2 == 1)
                    {
                        transform.GetChild(i).gameObject.GetComponent<RushObstacleFadeOut>().fadeOut = true;
                    }

                    if (childTimer >= 4)
                    {
                        for (int j = 0; j < 10; j++)
                        {
                          
                              transform.GetChild(j).gameObject.SetActive(true);
                        }

                        childTimer = 0;
                        isEven = !isEven;
                    }
                }

            }
        }
    }
}
