using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BlinkLaserController : MonoBehaviour
{
    public float interval_A = 2.0f; 
    public GameObject[] BlinkLaser_A = new GameObject[4];

    private float timer_A = 0.0f;
    private bool isLaserActive_A = true;


    public float interval_B = 1.5f;
    public GameObject[] BlinkLaser_B = new GameObject[3];

    private float timer_B = 0.0f;
    private bool isLaserActive_B = true;

    // Start is called before the first frame update
    void Start()
    {
        timer_A = interval_A;
        timer_B = interval_B;
    }

    // Update is called once per frame
    void Update()
    {
        timer_A -= Time.deltaTime;
        timer_B -= Time.deltaTime;        
       
        if (timer_A <= 0.0f)
        {
            // Debug.Log("활성화or비활성화!");
            ToggleLaser_A();
            timer_A = interval_A;
        }

        if (timer_B <= 0.0f)
        {
            ToggleLaser_B();
            timer_B = interval_B;
        }
    }

    private void ToggleLaser_A()
    {
        isLaserActive_A = !isLaserActive_A;
        for (int i = 0; i < 4; i++)
            BlinkLaser_A[i].SetActive(isLaserActive_A);
    }

    private void ToggleLaser_B()
    {
        isLaserActive_B = !isLaserActive_B;
        for (int i = 0; i < 3; i++)
            BlinkLaser_B[i].SetActive(isLaserActive_B);
    }
}
