using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGenerator : MonoBehaviour
{
    public GameObject BulletPrefab;
    float span = 1f;
    float delta = 0;

    public bool isShoot = false;

    public bool isBossOn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.delta += Time.deltaTime;
        if (this.delta > this.span && isShoot && isBossOn)
        {
            this.delta = 0;
            GameObject go = Instantiate(BulletPrefab);
            GetComponent<AudioSource>().Play();
            int px = Random.Range(232, 250);
            go.transform.position = new Vector3(px, 6, -1);      
        }

        if (GameManager.Instance.isClear)
        {
            isBossOn = false;
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isBossOn = true;
        }
    }
}
