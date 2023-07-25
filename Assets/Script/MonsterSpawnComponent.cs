using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumSpace;

public class MonsterSpawnComponent : MonoBehaviour
{
    // Start is called before the first frame update

    public bool isInfiniteSpawn = false; // 무한모드
    public bool isStartSpawn = false;

    public int spawnCnt = 0; // 몬스터 수
    public int maxSpawnCnt = 0; // 최대 몬스터 수
    public float spawnTime = 2f; // 한마리당 시간

    public GameObject[] monsters;

    public int doorNum = 0; // 패턴 다르게 할 가능성이 있어서 문 번호

    public float spawnTimer = 0f; // 스폰 타이머


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStartSpawn) return;

        if (!isInfiniteSpawn && spawnCnt >= maxSpawnCnt) return;

        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnTime)
        {
            if (doorNum == 1)
                Spawn(MonsterType.ATTACKROBOT);

            spawnCnt++;
            spawnTimer = 0;
        }
    }

    public void Spawn(MonsterType type)
    {
        GameObject monster = Instantiate(monsters[(int)type] , transform.position - new Vector3(0,0.15f,-2f), Quaternion.identity) as GameObject;
        monster.GetComponent<MonsterComponent>().type = type;
    }
}
