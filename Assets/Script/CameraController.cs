using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject Player;


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

        yPos = Mathf.Clamp(yPos, -21f, 5f);
        xPos = Mathf.Clamp(xPos, -0.01f, 264.1f);

        if (Player.transform.position.x <= 99.5f) // Player�� Stage2 ������ ������
            yPos = 0f;
        transform.position = new Vector3(xPos, yPos, transform.position.z);
    }


    private void ResolutionFix()
    {
        // ���� ���� ����
        float targetWidthAspect = 16.0f;
        float targetHeightAspect = 9.0f;

        Camera.main.aspect = targetWidthAspect / targetHeightAspect;

    }

}
