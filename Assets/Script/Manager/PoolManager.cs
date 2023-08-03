using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance = null; // ��𼭵� ������ �� �ֵ��� �ν��Ͻ� ����

    // �����յ��� ������ ����
    public GameObject[] EnemyPrefabs;
    public GameObject[] BulletPrefabs;

    // Ǯ ����� �ϴ� ����Ʈ��
    List<GameObject>[] EnemyPools;
    List<GameObject>[] BulletPools;

    private void Awake()
    {
        if (Instance) // �̹� Instance�� �Ҵ�Ȱ� ������ �ߺ��Ǵ� �� �����
        {
            DestroyImmediate(this.gameObject);
            return;
        }
        Instance = this; // Instance �Ҵ��� �� �Ǿ� �ִ� ��� ���� ���� �Ҵ�

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
            // ��Ȱ��ȭ�� ���ӿ�����Ʈ ã��
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
            // ��Ȱ��ȭ�� ���ӿ�����Ʈ ã��
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
