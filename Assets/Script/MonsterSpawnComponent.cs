using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumSpace;

public class MonsterSpawnComponent : MonoBehaviour
{
    // Start is called before the first frame update

    public bool isInfiniteSpawn = false; // ���Ѹ��
    public bool isStartSpawn = false;

    public int spawnCnt = 0; // ���� ��
    public int maxSpawnCnt = 0; // �ִ� ���� ��
    public float spawnTime = 2f; // �Ѹ����� �ð�

    public GameObject[] monsters;

    public int doorNum = 0; // ���� �ٸ��� �� ���ɼ��� �־ �� ��ȣ

    public float spawnTimer = 5f; // ���� Ÿ�̸�

    bool isInit = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStartSpawn) return;

        if (!isInfiniteSpawn && spawnCnt >= maxSpawnCnt) return;
        if (!isInit)
        {
            PoolManager.Instance.currentEnemyCnt = maxSpawnCnt;
            isInit = true;
        }

        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnTime)
        {
            if (doorNum == 1)
                Spawn(MonsterType.ATTACKROBOT);

            spawnCnt++;
            spawnTimer = 0;
        }

        if (spawnCnt == maxSpawnCnt)
            gameObject.GetComponent<Animator>().SetBool("isDoorOpen", false);
    }

    public void Spawn(MonsterType type)
    {
        //GameObject monster = Instantiate(monsters[(int)type] , transform.position - new Vector3(0,0.15f,-2f), Quaternion.identity) as GameObject;
        GameObject monster = PoolManager.Instance.enemyGet((int)type, transform.position - new Vector3(0, 0.15f, -2f));
        monster.GetComponent<MonsterComponent>().originPos = monster.transform.position;
        monster.GetComponent<MonsterComponent>().type = type;
    }
}
