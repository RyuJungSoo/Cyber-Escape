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
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position); // ���� ��ġ�� ���� ī�޶� ����(���� �ϴ��� 0,0 ���� ����� 1,1)�� �ش��ϴ� ��ǥ�� ��ȯ



        if (pos.x < -0.2f) 
            pos.x = 1.2f;


        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }
}
