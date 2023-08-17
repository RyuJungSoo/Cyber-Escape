using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject Player;

    //private Transform target;   //���� Ÿ��

    //private Vector3 lastMovingVelocity;
    //private Vector3 targetPosition;

    //private Camera cam;
    //private float targetZoomSize = 5f;
    //private float lastZoomSpeed;


    void Start()
    {
        ResolutionFix();

    }

    // Update is called once per frame
    void Update()
    {
        float xPos = Player.transform.position.x;
        float yPos = Player.transform.position.y;

        //ī�޶� position ����

        yPos = Mathf.Clamp(yPos, -21f, 5f); //(value, min, max)
        xPos = Mathf.Clamp(xPos, -0.01f, 264.1f);

        if (Player.transform.position.x <= 99.5f) // Player�� Stage2 ������ ������
            yPos = 0f;
        transform.position = new Vector3(xPos, yPos, transform.position.z);

        /*if (Player.transform.position.x >= 230.5)
        {
            target = GameObject.Find("Boss").GetComponent<Transform>();
            Move();
            Zoom();
            transform.position = new Vector3(240.5f, 0f, transform.position.z);
        }*/

    }


    private void ResolutionFix()
    {
        // ���� ���� ����
        float targetWidthAspect = 16.0f;
        float targetHeightAspect = 9.0f;

        Camera.main.aspect = targetWidthAspect / targetHeightAspect;

    }

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
}