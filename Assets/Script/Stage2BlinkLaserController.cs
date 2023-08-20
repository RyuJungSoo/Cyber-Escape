using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2BlinkLaserController : MonoBehaviour
{
    public float interval_A = 2.0f;
    private float timer_A = 0.0f;
    private bool isLaserActive_A = true;

    public float interval_B = 1.5f;
    private float timer_B = 0.0f;
    private bool isLaserActive_B = true;

    public GameObject BlinkLaser2_1;
    public GameObject BlinkLaser2_2;
    public GameObject BlinkLaser2_3;

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

        BlinkLaser2_1.SetActive(isLaserActive_A);
        BlinkLaser2_3.SetActive(isLaserActive_A);
    }

    private void ToggleLaser_B()
    {
        isLaserActive_B = !isLaserActive_B;

        BlinkLaser2_2.SetActive(isLaserActive_B);
    }
}
