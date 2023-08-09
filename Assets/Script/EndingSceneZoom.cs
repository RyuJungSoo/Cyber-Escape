using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingSceneZoom : MonoBehaviour
{
    public Camera Cam;

    public Vector3 Target;

    public float Speed;

    // Start is called before the first frame update
    void Start()
    {
        Cam = Camera.main;
    }

    // Update is called once per frame
    public void LateUpdate()
    {
        Cam.orthographicSize = Mathf.Lerp(Cam.orthographicSize, 5, Speed);
        Cam.transform.position = Vector3.Lerp(Cam.transform.position, Target, Speed);
    }
}
