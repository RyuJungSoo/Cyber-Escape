using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCompleteEventArgs
{
    public GameObject targetObject;
    public Vector3 position;
    public Quaternion quaternion;
}
public class CameraMoveToObject: MonoBehaviour
{
    public static event System.EventHandler<MoveCompleteEventArgs> EventHandler_CameraMoveTarget;
    public GameObject cam;
    //줌인 대상 오브젝트
    private Transform targetObject;
    //경유 지점 위치
    public Transform subTarget;

    //부드럽게 이동될 감도
    public float smoothTime = 0.3f;

    private Vector3 velocity = Vector3.zero;
    //카메라 타겟 줌인 상태 플래그
    public static bool IsActive = false;
    //줌인 정도 -가 클 수록 줌 아웃
    public float zoomIn = -5;
    //오브젝트 크기에 맞춰 줌기능 사용시 사용될 데이터
    private Bounds boundsData;
    private bool isBounds = true;
    //경유지점 도착 후 진행 카운트
    private int PassCount = 0;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsActive)
        {
            Vector3 targetPosition;

            if (subTarget != null && PassCount == 0)
            {
                targetPosition = subTarget.transform.position;
                smoothTime = 0.1f;
            }
            else
            {
                //경유지점 없을 시 bounds 체크 후 목표지점을 종착지로 설정
                if (!isBounds)
                    targetPosition = targetObject.TransformPoint(new Vector3(0, 10, zoomIn));
                else
                    targetPosition = new Vector3(boundsData.center.x, boundsData.center.y + boundsData.size.y, boundsData.center.z - boundsData.size.z + zoomIn);
            }

            //위에서 설정된 위치로 부드럽게 이동
            cam.transform.position = Vector3.SmoothDamp(cam.transform.position, targetPosition, ref velocity, smoothTime);
            cam.transform.LookAt(targetObject);

            //목표지점 이내에 도착
            if (Vector3.Distance(targetPosition, cam.transform.position) < 0.1f)
            {
                //경유 지점이 있을 경우
                if(subTarget != null)
                {
                    if(targetPosition == subTarget.transform.position)
                    {
                        PassCount++;

                        if (!isBounds)
                            targetPosition = targetObject.TransformPoint(new Vector3(0, 10, zoomIn));
                        else
                            targetPosition = new Vector3(boundsData.center.x, boundsData.center.y + boundsData.size.y, boundsData.center.z - boundsData.size.z + zoomIn);

                    }
                    else
                    {
                        //경유하고 최종 목적지 도착했을 떄 이벤트 처리
                        MoveCompleteEventArgs args = new MoveCompleteEventArgs();
                        args.targetObject = targetObject.gameObject;
                        args.position = cam.transform.position;
                        args.quaternion = cam.transform.rotation;
                        EventHandler_CameraMoveTarget(this, args);

                        Clear();

                    }
                }
                else
                {
                    // 경유 지점 없이 최종 목적지 도착했을 때 이벤트 처리
                    MoveCompleteEventArgs args = new MoveCompleteEventArgs();
                    args.targetObject = targetObject.gameObject;
                    args.position = cam.transform.position;
                    args.quaternion = cam.transform.rotation;
                    EventHandler_CameraMoveTarget(this, args);

                    Clear();
                }
            }
        }
    }

    public void SetTarget(GameObject target, bool bounds = true)
    {
        if (target == null)
            return;
        IsActive = true;
        targetObject = target.transform;

        //bounds가 true일 경우 target의 bounds 데이터를 저장
        if (bounds)
        {
            Bounds combinedBounds = new Bounds();
            var renderers = target.GetComponentsInChildren<Renderer>();
            foreach(var render in renderers)
            {
                combinedBounds.Encapsulate(render.bounds);
            }

            boundsData = combinedBounds;
            isBounds = true;
        }
        else
        {
            boundsData = new Bounds();
            isBounds = false;
        }

    }

    public void Clear()
    {
        smoothTime = 0.3f;
        IsActive = false;
        targetObject = null;
        PassCount = 0;
    }


}
