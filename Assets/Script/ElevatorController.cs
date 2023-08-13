using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    public Transform player;
    public Transform elevatorswitch;
    public Transform downpos;
    public Transform upperpos;

    public float speed;
    bool iselevatordown;

    // Start is called before the first frame update
    void Start()
    {
        iselevatordown = false;
    }

    // Update is called once per frame
    void Update()
    {
        StartElevator();
    }

    void StartElevator()
    {
        if(Vector2.Distance(player.position, elevatorswitch.position) < 0.5f)
        {
            if (transform.position.y <= downpos.position.y)
            {
                iselevatordown = true;
            }
        }

        if (iselevatordown)
        {
            transform.position = Vector2.MoveTowards(transform.position, upperpos.position, speed * Time.deltaTime);
        }
    }
}
