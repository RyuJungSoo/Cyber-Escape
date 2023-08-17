using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGenerator : MonoBehaviour
{
    public GameObject BulletPrefab;
    float span = 3.0f;
    float delta = 0;

    GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        this.Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        this.delta += Time.deltaTime;
        if (this.delta > this.span)
        {
            this.delta = 0;
            if (this.Player.transform.position.x > 230f)
            {
                GameObject go = Instantiate(BulletPrefab);
                GetComponent<AudioSource>().Play();
                int px = Random.Range(232, 250);
                go.transform.position = new Vector3(px, 6, -1);
            }
        }
    }
}
