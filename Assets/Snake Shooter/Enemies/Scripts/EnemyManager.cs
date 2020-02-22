using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("Options")]
    [SerializeField] private List<Enemy> enemyPrefabs;
    [SerializeField] private float spawnDelay;

    private const float INITIAL_SPAWN_DELAY = 1.5f;

    private const float SPAWN_DELAY_GROWTH_FACTOR = 0.0005f;

    private int poolIndex;
    private List<Enemy> enemyPool;

    public static event Action<Enemy> OnEnemyDefeated;
    public static event Action<Enemy> OnEnemyHasReachedBase;


    private void Awake()
    {
        enemyPool = new List<Enemy>();

        CreateEnemy(enemyPrefabs[0]);

        for (int i = 0; i < 30; i++)
        {
            var prefab = RandomlySelectPrefab();
            CreateEnemy(prefab);
        }
    }

    private Enemy RandomlySelectPrefab()
    {
        var rand = UnityEngine.Random.Range(0.0f, 1.0f);

        if (rand <= .7) return enemyPrefabs[0];
        if (rand <= .9) return enemyPrefabs[1];
        else return enemyPrefabs[2];
       
    }

    private void CreateEnemy(Enemy prefab )
    {
        var enemy = Instantiate(prefab, transform.position, Quaternion.identity, transform);
        enemyPool.Add(enemy);
        enemy.gameObject.SetActive(false);

        enemy.Health.OnHealthZero += () =>
        {
            OnEnemyDefeated?.Invoke(enemy);
        };

        enemy.OnEnemyHasReachedBase += () =>
        {
            OnEnemyHasReachedBase?.Invoke(enemy);
        };
    }

    private void OnEnable()
    {
        LevelManager.OnLevelBegun += OnLevelBegun;

        StartCoroutine(EnemySpawnUpdate(1));
    }

    private void OnDisable()
    {
        LevelManager.OnLevelBegun -= OnLevelBegun;

    }

    private void ClearAllEnemies()
    {
        enemyPool.ForEach((enemy) =>
        {
            enemy.gameObject.SetActive(false);
        });
    }

    private void OnLevelBegun(int level)
    {
        StopAllCoroutines();
        ClearAllEnemies();

        enemyPool.ForEach((enemy) =>
        {
            enemy.Speed.Value = enemy.Speed.BaseValue + enemy.Speed.Growth * (level - 1);
            enemy.Health.Max = enemy.Health.BaseMax + enemy.Health.Growth * (level - 1);
        });

        spawnDelay =  1.0f / (0.5f + ((level - 1) * SPAWN_DELAY_GROWTH_FACTOR));

        StartCoroutine(EnemySpawnUpdate(level));
    }

    private IEnumerator EnemySpawnUpdate(int enemyCount)
    {
        yield return new WaitForSeconds(INITIAL_SPAWN_DELAY);

        for (int i = 0; i < enemyCount; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private void SpawnEnemy()
    {
        Enemy enemy;

        do
        {
            enemy = enemyPool[poolIndex];
            poolIndex = (poolIndex + 1) % enemyPool.Count;
        }
        while (enemy.gameObject.activeSelf);
        

        enemy.gameObject.SetActive(true);
    }
}
