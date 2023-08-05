using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BlinkLaserController : MonoBehaviour
{
    public float interval = 2.0f;
    public GameObject BlinkLaser;

    private float timer = 0.0f;
    private bool isLaserActive = true;  

    // Start is called before the first frame update
    void Start()
    {
        timer = interval;
        this.BlinkLaser = GameObject.Find("BlinkLaser");
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        
        if (timer <= 0.0f)
        {
            // Debug.Log("활성화or비활성화!");
            ToggleLaser();
            timer = interval;
        }
    }

    private void ToggleLaser()
    {
        isLaserActive = !isLaserActive;
        BlinkLaser.SetActive(isLaserActive);
    }
}
