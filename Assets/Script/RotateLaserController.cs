using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateLaserController : MonoBehaviour
{
    public float rotSpeed = 1.0f;
    public float IsClockwise = 1.0f; //�ð� ������ 1, �ݽð�����̸� -1�� �������ּ���

    

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
