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
    //���� ��� ������Ʈ
    private Transform targetObject;
    //���� ���� ��ġ
    public Transform subTarget;

    //�ε巴�� �̵��� ����
    public float smoothTime = 0.3f;

    private Vector3 velocity = Vector3.zero;
    //ī�޶� Ÿ�� ���� ���� �÷���
    public static bool IsActive = false;
    //���� ���� -�� Ŭ ���� �� �ƿ�
    public float zoomIn = -5;
    //������Ʈ ũ�⿡ ���� �ܱ�� ���� ���� ������
    private Bounds boundsData;
    private bool isBounds = true;
    //�������� ���� �� ���� ī��Ʈ
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
                //�������� ���� �� bounds üũ �� ��ǥ������ �������� ����
                if (!isBounds)
                    targetPosition = targetObject.TransformPoint(new Vector3(0, 10, zoomIn));
                else
                    targetPosition = new Vector3(boundsData.center.x, boundsData.center.y + boundsData.size.y, boundsData.center.z - boundsData.size.z + zoomIn);
            }

            //������ ������ ��ġ�� �ε巴�� �̵�
            cam.transform.position = Vector3.SmoothDamp(cam.transform.position, targetPosition, ref velocity, smoothTime);
            cam.transform.LookAt(targetObject);

            //��ǥ���� �̳��� ����
            if (Vector3.Distance(targetPosition, cam.transform.position) < 0.1f)
            {
                //���� ������ ���� ���
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
                        //�����ϰ� ���� ������ �������� �� �̺�Ʈ ó��
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
                    // ���� ���� ���� ���� ������ �������� �� �̺�Ʈ ó��
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

        //bounds�� true�� ��� target�� bounds �����͸� ����
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
