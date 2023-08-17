using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGenerator : MonoBehaviour
{
    public GameObject BulletPrefab;
    float span = 1f;
    float delta = 0;

    public bool isShoot = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.delta += Time.deltaTime;
        if (this.delta > this.span && isShoot)
        {
            this.delta = 0;
            GameObject go = Instantiate(BulletPrefab);
            int px = Random.Range(232, 250);
            go.transform.position = new Vector3(px, 6, -1);      
        }
    }
}
