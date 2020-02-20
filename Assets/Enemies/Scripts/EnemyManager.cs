using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LevelManager progressManager;
    [SerializeField] private GameOverManager gameOverManager;

    [Header("Options")]
    [SerializeField] private List<Enemy> enemyPrefabs;
    [SerializeField] private float spawnDelay;

    private int poolIndex;
    private List<Enemy> enemyPool;

    private Coroutine spawnCoroutine;

    public event Action<Enemy> OnEnemyDefeated;

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
    }

    private void Start()
    {
        progressManager.OnLevelChanged += OnLevelChanged;
        gameOverManager.OnRevive += OnRevive;

        spawnCoroutine = StartCoroutine(EnemySpawnUpdate(1));
    }

    private void OnRevive()
    {
        ClearAllEnemies();
        StopCoroutine(spawnCoroutine);
    }

    private void ClearAllEnemies()
    {
        enemyPool.ForEach((enemy) =>
        {
            enemy.gameObject.SetActive(false);
        });
    }

    private void OnLevelChanged(int level)
    {
        enemyPool.ForEach((enemy) =>
        {
            enemy.Speed.Value = enemy.Speed.BaseValue + enemy.Speed.Growth * level;
            enemy.Health.Max = enemy.Health.BaseMax + enemy.Health.Growth * level;
        });

        spawnDelay =  1.0f / (1 + (level * 0.01f));

        spawnCoroutine = StartCoroutine(EnemySpawnUpdate(level));
    }

    private IEnumerator EnemySpawnUpdate(int level)
    {
        for(int i = 0; i < level; i++)
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
