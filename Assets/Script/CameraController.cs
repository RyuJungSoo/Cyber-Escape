using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform Target;
    public float Speed;

    //private Transform target;   //���� Ÿ��

    //private Vector3 lastMovingVelocity;
    //private Vector3 targetPosition;

    private Camera cam;
    //private float targetZoomSize = 5f;
    //private float lastZoomSpeed;


    private void ResolutionFix()
    {
        // ���� ���� ����
        float targetWidthAspect = 16.0f;
        float targetHeightAspect = 9.0f;

        Camera.main.aspect = targetWidthAspect / targetHeightAspect;

    }
    void Start()
    {
        ResolutionFix();
        cam.orthographicSize = 5f;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //transform.position = new Vector3(Target.position.x,Target.position.y,-12f);
        transform.position = Vector3.Lerp(transform.position, Target.position, Time.deltaTime * Speed);

        float xPos = Target.transform.position.x;
        float yPos = Target.transform.position.y;

        //ī�޶� position ����
        xPos = Mathf.Clamp(xPos, 0.43f, 264.1f);
        yPos = Mathf.Clamp(yPos, 0.02f, 1f); //(value, min, max)

        //Stage2 �Ʒ� ��ֹ� ����: Ȯ��
        if (Target.transform.position.y <= -3f)
        {
            cam.orthographicSize = 3.5f;
            yPos = Mathf.Clamp(yPos, -16f, -14.47f); //(value, min, max)
        }

        // Stage2 ���� ���񱸰�: ȭ�� Ȯ�� �� ���������� �� ���̸� �� ��
        if (Target.transform.position.x > 135 && Target.transform.position.y >= -3)
        {
            cam.orthographicSize = 3.5f;
            xPos = Mathf.Clamp(xPos, 0.43f, 147.32f);
            yPos = Mathf.Clamp(yPos, -13.9f, -1.3f); //(value, min, max)
        }

        transform.position = new Vector3(xPos, yPos, -12f);

        //���� ��������
        if (Target.transform.position.x >= 231 && Target.transform.position.x < 250f)
            transform.position = new Vector3(240.5f, -1f, transform.position.z);

        
    }
}
     

        /*if 
        {
            target = GameObject.Find("Boss").GetComponent<Transform>();
            Move();
            Zoom();
            transform.position = new Vector3(240.5f, 0f, transform.position.z);
        }*/


    /*
    //ī�޶� ���� ������� �̵�
    private void Move()
    {
        targetPosition = target.transform.position;

        //Vector3.SmoothDamp(���� ��ġ, ���� ���� ��ġ, ������ ������ ��ȭ��, �����ð�); 
        Vector3 smoothPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref lastMovingVelocity, 0.2f);

        transform.position = smoothPosition;

    }

    private void Zoom()
    {
        float smoothZoomSize = Mathf.SmoothDamp(cam.orthographicSize, targetZoomSize, ref lastZoomSpeed, 0.2f);
        cam.orthographicSize = smoothZoomSize;
    }*/

