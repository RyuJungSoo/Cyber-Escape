using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageTriggerComponent : MonoBehaviour
{
    public bool isFirstStage;
    public GameObject[] Doors;
    public float movementSpeed = 3;
    public float IntervalTime = 2;
    public Camera cam;

    private int index = 0;
    private Vector3 OriginPos;
    private Vector3 targetposition;
    private bool MoveStart;
    private bool isEnd;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (isEnd == true)
            return;

        if (MoveStart)
        {
            targetposition = new Vector3(Doors[index].transform.position.x, Doors[index].transform.position.y, -12);

            // 현재 카메라 위치에서 타겟 위치로 부드럽게 이동
            Vector3 newPosition = Vector3.Lerp(cam.transform.position, targetposition, Time.unscaledDeltaTime * movementSpeed);
            cam.transform.position = newPosition;


        }

        if (Vector3.Distance(targetposition, cam.transform.position) < 0.1f)
        {
            //Debug.Log("Finish");
            if(index <= Doors.Length - 2)
                StartCoroutine(Timer(IntervalTime));
            index++;
            MoveStart = true;
        }

        if (index > Doors.Length - 1)
        {
            isEnd = true;
            Time.timeScale = 1;
            cam.GetComponent<CameraController>().enabled = true;
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(isFirstStage)
            GameManager.Instance.ChangeBGM(1);
        OriginPos = cam.transform.position;
        Time.timeScale = 0;
        cam.GetComponent<CameraController>().enabled = false;
        //Debug.Log("moveStart");

        MoveStart = true;


        //this.gameObject.SetActive(false);
    }

    IEnumerator Timer(float Time)
    {
        MoveStart = false;
        yield return new WaitForSecondsRealtime(Time);

    }
}
