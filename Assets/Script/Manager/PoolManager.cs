using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance = null; // 어디서든 접근할 수 있도록 인스턴스 선언

    // 프리팹들을 보관할 변수
    public GameObject[] EnemyPrefabs;
    public GameObject[] BulletPrefabs;

    // 풀 담당을 하는 리스트들
    List<GameObject>[] EnemyPools;
    List<GameObject>[] BulletPools;

    private void Awake()
    {
        if (Instance) // 이미 Instance에 할당된게 있으면 중복되는 걸 지우기
        {
            DestroyImmediate(this.gameObject);
            return;
        }
        Instance = this; // Instance 할당이 안 되어 있는 경우 현재 것을 할당

        EnemyPools = new List<GameObject>[EnemyPrefabs.Length];

        for (int i = 0; i < EnemyPools.Length; i++)
            EnemyPools[i] = new List<GameObject>();

        BulletPools = new List<GameObject>[BulletPrefabs.Length];

        for (int i = 0; i < BulletPools.Length; i++)
            BulletPools[i] = new List<GameObject>();
    }

    public GameObject enemyGet(int index, Vector3 pos)
    {
        GameObject select = null;
        int curEnemyIndex = 0;

        foreach (GameObject item in EnemyPools[index])
        {
            // 비활성화된 게임오브젝트 찾기
            if (!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                select.GetComponent<MonsterComponent>().Reset();
                select.transform.position = pos;
                break;

            }
            curEnemyIndex++;
        }

        if (select == null)
        {
            select = Instantiate(EnemyPrefabs[index], pos, Quaternion.identity, transform);
            EnemyPools[index].Add(select);
        }

        return select;
    }

    public GameObject BulletGet(int index, Vector3 pos)
    {
        GameObject select = null;
        int curEnemyIndex = 0;

        foreach (GameObject item in BulletPools[index])
        {
            // 비활성화된 게임오브젝트 찾기
            if (!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                select.transform.position = pos;
                break;

            }
            curEnemyIndex++;
        }

        if (select == null)
        {
            select = Instantiate(BulletPrefabs[index], pos, Quaternion.identity, transform);
            BulletPools[index].Add(select);
        }

        return select;
    }
}
