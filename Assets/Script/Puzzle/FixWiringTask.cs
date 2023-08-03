using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public enum EWireColor
{ 
    None = -1,
    Red,
    Blue,
    Yellow,
    Magenta
}

public class FixWiringTask : MonoBehaviour
{
    [SerializeField]
    private List<LeftWire> mLeftWires;

    [SerializeField]
    private List<RightWire> mRightWires;

    [SerializeField]
    private LeftWire mSelectedWire;

    public int correctNum = 0;

    private void OnEnable()
    {
        List<int> numberPool = new List<int>();
        for(int i = 0; i < 4; i++)
        {
            numberPool.Add(i);
        }

        int index = 0;
        while(numberPool.Count != 0)
        {
            var number = numberPool[Random.Range(0, numberPool.Count)];
            mLeftWires[index++].SetWireColor((EWireColor)number);
            numberPool.Remove(number);
        }

        for (int i = 0; i < 4; i++)
        {
            numberPool.Add(i);
        }

        index = 0;
        while (numberPool.Count != 0)
        {
            var number = numberPool[Random.Range(0, numberPool.Count)];
            mRightWires[index++].SetWireColor((EWireColor)number);
            numberPool.Remove(number);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Input.mousePosition, Vector2.right, 1f);
            if (hit.collider != null && hit.collider.CompareTag("TaskWireL"))
            {
                var left = hit.collider.GetComponentInParent<LeftWire>();
                if (left != null)
                {
                    if (!hit.collider.GetComponentInParent<LeftWire>().IsConnected)
                    mSelectedWire = left;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (mSelectedWire != null)
            {

                Collider2D[] hits = Physics2D.OverlapCircleAll(mSelectedWire.gameObject.transform.GetChild(0).position + new Vector3(25, 0f),5);
                //RaycastHit2D[] hits = Physics2D.RaycastAll(Input.mousePosition, Vector2.right, 1f);
                foreach (var hit in hits)
                {
                    if (hit != null && hit.CompareTag("TaskWireR"))
                    {
                        var right = hit.GetComponentInParent<RightWire>();
                        if (right != null)
                        {
                           
                            mSelectedWire.ConnectWire(right);
                            right.ConnectWire(mSelectedWire);
                            mSelectedWire = null;
                            if (right.IsConnected) correctNum++;
                            if (correctNum == 4)
                            {
                                gameObject.GetComponent<PuzzleCompononent>().isSolved = true;
                                gameObject.GetComponent<PuzzleCompononent>().isFailed = false;
                                gameObject.SetActive(false);

                            }
                            return;
                        }
                    }
                }

                mSelectedWire.ResetTarget();
                mSelectedWire.DisconnectWire();
                mSelectedWire = null;
            }
        }

        if (mSelectedWire != null)
        {
            mSelectedWire.SetTarget(Input.mousePosition, -15f);
        }

    }
}
