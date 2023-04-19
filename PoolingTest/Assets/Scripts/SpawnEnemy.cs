using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private GameObject[] SpawnSpots;
    [SerializeField] private GameObject Enemyprefab;
    private float SpawnTime = 1.3f;
    private Queue<GameObject> enemyPool = new Queue<GameObject>();

    void Start()
    {
        SpawnSpots = GameObject.FindGameObjectsWithTag("SpawnSpot");
        InitializeEnemyPool();
        StartCoroutine(SpawnEnemies());
    }

    void InitializeEnemyPool()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject enemy = Instantiate(Enemyprefab);
            enemy.SetActive(false);
            enemyPool.Enqueue(enemy);
        }
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            int RandomIndex = Random.Range(0, SpawnSpots.Length);
            Vector3 SpawnPos = SpawnSpots[RandomIndex].transform.position;
            SpawnEnemyMark(SpawnPos);
            yield return new WaitForSeconds(SpawnTime);
        }
    }

    void SpawnEnemyMark(Vector3 enemy)
    {
        if (enemyPool.Count > 0)
        {
            GameObject enemyObject = enemyPool.Dequeue();
            enemyObject.transform.position = enemy;
            enemyObject.SetActive(true);
        }
        else
        {
            GameObject enemyObject = Instantiate(Enemyprefab);
            enemyObject.transform.position = enemy;
            enemyPool.Enqueue(enemyObject);
        }
    }
}
