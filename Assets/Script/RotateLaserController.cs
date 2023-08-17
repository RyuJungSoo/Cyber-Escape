using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateLaserController : MonoBehaviour
{
    public float rotSpeed = 1.0f;
    public float IsClockwise = 1.0f; //시계 방향은 1, 반시계방향이면 -1로 설정해주세요

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, IsClockwise * this.rotSpeed);
    }
}
