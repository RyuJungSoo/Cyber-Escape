using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform Target;   //추적 타깃

    public float smoothTime = 0.2f;
    public float Speed;

    public Camera cam;
    private Vector3 CamPos = new Vector3(0f, 0f, -12f);
    private float TargetZoomSize = 5f;
    private const float BossZoomSize = 6f;
    private const float ElevatorZoomSize = 3f;
    private const float SpecialZoomSize = 3.5f;

    private float lastZoomSpeed;

    public enum Stage
    {
        //  스테이지 구간 유형 나누기
        TutorialStage1, Base, Stage2Puzzle1, Stage2Puzzle2, ElevatorSection, Boss
    }
    private Stage state
    {
        set
        {
            switch (value)
            {
                case Stage.TutorialStage1:
                    TargetZoomSize = 5f; //추가
                    break;
                case Stage.Base:
                    TargetZoomSize = 5f;
                    break;
                case Stage.Stage2Puzzle1:
                    TargetZoomSize = SpecialZoomSize; //추가
                    break;
                case Stage.Stage2Puzzle2:
                    TargetZoomSize = SpecialZoomSize;
                    break;//추가
                case Stage.ElevatorSection:
                    TargetZoomSize = ElevatorZoomSize;
                    break;
                case Stage.Boss:
                    TargetZoomSize = BossZoomSize; //추가
                    break;
            }
        }
    }


    /*void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        { }
    }*/

    void Awake()
    {
        state = Stage.Base;
    }

    private void ResolutionFix()
    {
        // 가로 세로 비율
        float targetWidthAspect = 16.0f;
        float targetHeightAspect = 9.0f;

        Camera.main.aspect = targetWidthAspect / targetHeightAspect;

    }

    // Start is called before the first frame update
    void Start()
    {
        Target = GameObject.Find("Player").GetComponent<Transform>();
        ResolutionFix();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        TrackPlayerLimitCameraArea();
        Zoom();

    }

    void TrackPlayerLimitCameraArea()
    {
        // 타깃(플레이어) 추적
        transform.position = Vector3.Lerp(transform.position, Target.position + CamPos, Time.deltaTime * Speed);

        float xPos = Target.transform.position.x;
        float yPos = Target.transform.position.y;

        //카메라 position 제한

        //Stage2 엘리베이터 구간
        if (Target.transform.position.x > 154.76f && Target.transform.position.x <= 159.13f
            && Target.transform.position.y < -2f)
        {
            state = Stage.ElevatorSection;
            xPos = Mathf.Clamp(xPos, 0.43f, 158.5f);
            yPos = Mathf.Clamp(yPos, -16f, -1f); //(value, min, max)
            Debug.Log("엘리베이터 방");
        }

        //Stage2 아래 장애물 구간: 확대
        else if (Target.transform.position.x > 122f && Target.transform.position.x <= 179f && Target.transform.position.y <= -14f)
        {
            state = Stage.Stage2Puzzle1;
            yPos = Mathf.Clamp(yPos, -16f, -12.47f); //(value, min, max)
            Debug.Log("장애물 구간");
        }

        // Stage2 위층 퍼즐구간: 화면 확대 및 엘리베이터 방 보이면 안 됨
        else if (Target.transform.position.x > 135f && Target.transform.position.x <= 165f && Target.transform.position.y >= -4f)
        {
            state = Stage.Stage2Puzzle2;
            xPos = Mathf.Clamp(xPos, 0.43f, 147.5f);
            yPos = Mathf.Clamp(yPos, -13.9f, -1.3f); //(value, min, max)
            Debug.Log("stage2 퍼즐1");
        }

        // tutorial~Stage1
        else if (Target.transform.position.x <= 100f)
        {
            state = Stage.TutorialStage1;
            xPos = Mathf.Clamp(xPos, 0.43f, 264.1f);
            yPos = Mathf.Clamp(yPos, 0.02f, 1f); //(value, min, max)
            Debug.Log("stage1 이내");
        }

        // Stage2
        else if (Target.transform.position.x <= 165f)
        {
            state = Stage.Base;
            yPos = Mathf.Clamp(yPos, -21f, 1f);
            Debug.Log("stage2");
        }

        //Stage3 엘리베이터 구간
        else if (Target.transform.position.x > 173f && Target.transform.position.x <= 179.13f
            && Target.transform.position.y < 2f)
        {
            state = Stage.ElevatorSection;
            xPos = 176.5f;
            yPos = Mathf.Clamp(yPos, -16f, 2f); //(value, min, max)
            Debug.Log("엘리베이터 방");
        }

        //Stage3 장애물 구간
        else if (Target.transform.position.x >= 201f && Target.transform.position.x < 213f)
        {
            state = Stage.Stage2Puzzle1;
            yPos = yPos + 1;
            Debug.Log("stage3 장애물구간");
        }

        //Stage3
        else if (Target.transform.position.x < 201f)
        {
            state = Stage.Base;
            yPos = yPos + 2;
            Debug.Log("stage3");
        }

        else if (Target.transform.position.x >= 213f && Target.transform.position.x < 230.5f)
        {
            state = Stage.Base;
            yPos = Mathf.Clamp(yPos, -5f, 1f);
            Debug.Log("stage3");
        }

        else
        {
            state = Stage.Base;
            xPos = Mathf.Clamp(xPos, 0.43f, 264.1f);
            //yPos = Mathf.Clamp(yPos, -5f, 1f);
        }
        transform.position = new Vector3(xPos, yPos, -12f);

        //보스 스테이지
        if (Target.transform.position.x >= 230.5f && Target.transform.position.x < 250f)
        {
            state = Stage.Boss;
            transform.position = new Vector3(240.5f, -3.7f, -12f);
            Debug.Log("보스 스테이지 들어옴");
        }

        Debug.Log("카메라 추적, 제한 끝");
    }

    void Zoom()
    {
        float smoothZoomSize = Mathf.SmoothDamp(cam.orthographicSize, TargetZoomSize, ref lastZoomSpeed, smoothTime);
        cam.orthographicSize = smoothZoomSize;
        Debug.Log("줌");
    }

}