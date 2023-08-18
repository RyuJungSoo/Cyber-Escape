using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingSceneZoom : MonoBehaviour
{
    public Camera Cam;

    public GameObject EndingUI;

    public Vector3 Target;

    public float Speed;
    private bool isZoom;

    // Start is called before the first frame update
    void Start()
    {
        Cam = Camera.main;
    }

    // Update is called once per frame
    public void LateUpdate()
    {
        if (isZoom == false)
        {
            Cam.orthographicSize = Mathf.Lerp(Cam.orthographicSize, 5, Speed);
            Cam.transform.position = Vector3.Lerp(Cam.transform.position, Target, Speed);
        }

        if (Cam.orthographicSize >= 4.3f)
        {
            isZoom = true;
            StartCoroutine(Credit());
        }
        
    }

    IEnumerator Credit()
    {
        yield return new WaitForSeconds(2.5f);
        EndingUI.gameObject.SetActive(true);

    }


}
