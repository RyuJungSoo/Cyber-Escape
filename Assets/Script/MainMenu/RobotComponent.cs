using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotComponent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position); // 현재 위치를 메인 카메라 영역(좌측 하단이 0,0 우측 상단이 1,1)에 해당하는 좌표로 변환



        if (pos.x < -0.2f) 
            pos.x = 1.2f;


        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }
}
