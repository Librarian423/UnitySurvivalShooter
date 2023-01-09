using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Enemy[] enemyPrefab;

    public Transform[] spawnPoints;

    public int minCount = 3;
    public int maxCount = 6;

    public float spawnDelay = 5f;

    public float timeBetSpawnMax = 7f; // 최대 시간 간격
    public float timeBetSpawnMin = 2f; // 최소 시간 간격
    private float timeBetSpawn; // 생성 간격

    private float lastSpawnTime; // 마지막 생성 시점


    private List<Enemy> enemies = new List<Enemy>();
    private int enemyCount;

    private void Start()
    {
        // 생성 간격과 마지막 생성 시점 초기화
        timeBetSpawn = Random.Range(timeBetSpawnMin, timeBetSpawnMax);
        lastSpawnTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance != null && GameManager.instance.isGameover) 
        {
            return;
        }

        if (Time.time >= lastSpawnTime + timeBetSpawn )
        {
            // 마지막 생성 시간 갱신
            lastSpawnTime = Time.time;
            // 생성 주기를 랜덤으로 변경
            timeBetSpawn = Random.Range(timeBetSpawnMin, timeBetSpawnMax);
            // 아이템 생성 실행
            SpawnEnemies();
        }

    }

    private void SpawnEnemies()
    {
        int count = Random.Range(minCount, maxCount);

        for (int i = 0; i < count; i++)
        {
            CreateEnemy(Random.value);
        }

    }

    private void CreateEnemy(float intensity)
    {
        //var health = Mathf.Lerp(healthMin, healthMax, intensity);
        //var damage = Mathf.Lerp(damageMin, damageMax, intensity);
        //var speed = Mathf.Lerp(speedMin, speedMax, intensity);
        //var color = Color.Lerp(Color.white, strongEnemyColor, intensity);
        int type = Random.Range(0, enemyPrefab.Length);
        var point = spawnPoints[Random.Range(0, spawnPoints.Length)];
        var enemy = Instantiate(enemyPrefab[type], point.position, point.rotation);
        //var data = enemyDatas[Random.Range(0, enemyDatas.Length)];

        //enemy.Setup(data);
        enemies.Add(enemy);

        enemy.onDeath += () => enemies.Remove(enemy);
        enemy.onDeath += () => GameManager.instance.AddScore(100);
        enemy.onDeath += () => StartCoroutine(CoDestroyDelay(enemy.gameObject, 10f));
    }

    IEnumerator CoDestroyDelay(GameObject item, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (item != null)
        {
            Destroy(item);
        }
    }
}
