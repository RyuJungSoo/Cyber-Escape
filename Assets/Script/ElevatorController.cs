using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    public Transform player;
    public Transform elevatorswitch;
    public Transform downpos;
    public Transform upperpos;

    public bool isSwitch;

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
        if (isSwitch && Input.GetKeyDown("e"))
        {
            if (transform.position.y == downpos.position.y)
            {
                iselevatordown = true;
                GetComponent<AudioSource>().Play();
            }
            
            else if (transform.position.y == upperpos.position.y)
            {
                iselevatordown = false;
                GetComponent<AudioSource>().Play();
            }
            
        }
        else if (Vector2.Distance(player.position, elevatorswitch.position) > 5.0f)
        {
            iselevatordown = false;
        }

        if (iselevatordown)
        {
            transform.position = Vector2.MoveTowards(transform.position, upperpos.position, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, downpos.position, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isSwitch = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isSwitch = false;
        }
    }
}
