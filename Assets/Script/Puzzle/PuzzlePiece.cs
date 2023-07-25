using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzlePiece : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler 
{
    public float okAngle = 0;
    public float RotationAngle = 45;
    public int snapOffset = 30;
    public GameObject piecePos;
    public bool isSet;
    private bool isMauseDown;
    private GameObject Puzzle;

    // 드래그 시작
    public void OnBeginDrag(PointerEventData eventData)
    {
        isMauseDown = true;

    }

    // 드래그 중
    public void OnDrag(PointerEventData eventData)
    {
        if(isSet == false)
            transform.position = eventData.position;
        
    }

    // 드래그가 끝났을 때 
    public void OnEndDrag(PointerEventData eventData)
    {
        isMauseDown = false;
        // 퍼즐 구멍 위치와의 거리가 snapOffset보다 작고 회전 각도가 서로 같으면
        if (Vector3.Distance(piecePos.transform.position, transform.position) < snapOffset && piecePos.CompareTag(tag)) 
        {




            
            if (Mathf.Abs(Mathf.Round(transform.eulerAngles.z)) % okAngle == 0 || transform.rotation == piecePos.transform.rotation)
            {

                transform.SetParent(piecePos.transform);
                transform.localPosition = Vector3.zero;
                transform.rotation = piecePos.transform.rotation;
                isSet = true;

 
                
                Puzzle.GetComponent<PieceCheckComponent>().SetCheck();

            }
        }
    }

    void Start()
    {
        Puzzle = transform.parent.gameObject.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        // 퍼즐 조각 회전
        if (isMauseDown)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {

                transform.Rotate(new Vector3(0f, 0f, -45f));
                if (transform.eulerAngles.z >= 360)
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                else if(transform.eulerAngles.z < 0.1)
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                //Debug.Log(Mathf.Abs(transform.rotation.eulerAngles.z));


            }

            else if (Input.GetKeyDown(KeyCode.Q))
            {

                transform.Rotate(new Vector3(0f, 0f, 45f));
                if (transform.eulerAngles.z >= 360)
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                else if (transform.eulerAngles.z < 0.1)
                    transform.rotation = Quaternion.Euler(0, 0, 0);
               // Debug.Log(Mathf.Abs(transform.rotation.eulerAngles.z));

            }
            
        }

    }
}
