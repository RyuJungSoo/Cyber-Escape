using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RbulletGenerator : MonoBehaviour
{
    public GameObject RbulletPrefab;
    float span = 2.0f;
    float delta = 0;

    void Start()
    {
        
    }

    void Update()
    {
        this.delta += Time.deltaTime;
        if (this.delta > this.span)
        {
            this.delta = 0;
            GameObject go = Instantiate(RbulletPrefab);
            float py = Random.Range(-1.5f, 3.5f);
            go.transform.position = new Vector3(98.0f, py, -1);
        }
    }
}
