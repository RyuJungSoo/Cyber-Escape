using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PressComponent : MonoBehaviour
{
    // Start is called before the first frame update

    Vector3 originPos;
    ObstacleComponent obstacleComponent;

    public float downSpeed = 3.0f;
    public float upSpeed = 2.0f;
    public bool isDown = true;
    public bool is2Stage = false;


    void Start()
    {
        originPos = transform.position; 
        obstacleComponent = transform.GetChild(0).GetComponent<ObstacleComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!is2Stage)
            obstacleComponent.isNotAttacking = !isDown;
        else
            obstacleComponent.isNotAttacking = false;

        if (isDown)
        {
            transform.position += new Vector3(0, -1) * downSpeed * Time.deltaTime;
        }
        else
        {
            transform.position += new Vector3(0, 1) * upSpeed * Time.deltaTime;
            if (transform.position.y > originPos.y)
            {
                transform.position = originPos;
                isDown = true;
            }
        }

    }

 


}
