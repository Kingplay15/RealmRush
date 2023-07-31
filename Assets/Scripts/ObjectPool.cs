using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnTimer = 1f;

    private GameObject[] pool;
    [SerializeField] private int poolSize = 5;

    private void Awake()
    {
        PopulatePool();
    }

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private void PopulatePool()
    {
        pool = new GameObject[poolSize];

        for(int i=0;i<poolSize;i++)
        {
            pool[i] = Instantiate(enemyPrefab, transform);
            pool[i].SetActive(false);
        }
    }

    private void EnableObjectInPool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            if (pool[i].activeSelf == false)
            {
                pool[i].SetActive(true);
                return;
            }               
        }
    }

    private IEnumerator SpawnEnemies()
    {
        while(true)
        {
            EnableObjectInPool();
            yield return new WaitForSeconds(spawnTimer);
        }
    }
}
