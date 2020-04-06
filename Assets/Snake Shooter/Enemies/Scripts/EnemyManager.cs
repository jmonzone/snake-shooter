using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private float spawnDelay = 0;

    private ScriptableLevel currentLevel;

    private Dictionary<string, List<GameObject>> enemyPools;

    private const float INITIAL_SPAWN_DELAY = 1.5f;
    private const float SPAWN_DELAY_GROWTH_FACTOR = 0.0005f;

    public static event Action<GameObject> OnEnemyDefeated;

    private void CreateEnemies()
    {
        enemyPools = new Dictionary<string, List<GameObject>>();

        currentLevel.Rounds.ForEach((round) =>
        {
            round.Waves.ForEach((wave) =>
             {
                 var enemy = wave.Enemy;
                 var enemyKey = enemy.ID;

                 if (!enemyPools.ContainsKey(enemyKey))
                 {
                     enemyPools.Add(enemyKey, new List<GameObject>());
                 };

                 var pool = enemyPools[enemyKey];

                 while(wave.Count > pool.Count)
                 {
                     var enemyObject = CreateEnemy(enemy.Prefab);
                     pool.Add(enemyObject);
                 }
             });
        });
        
    }

    private GameObject CreateEnemy(GameObject prefab)
    {
        var enemy = Instantiate(prefab, transform.position, Quaternion.identity, transform);
        enemy.gameObject.SetActive(false);

        enemy.GetComponent<EnemyHealth>().OnHealthZero += () =>
        {
            OnEnemyDefeated?.Invoke(enemy);
        };

        return enemy;
    }

    private void OnEnable()
    {
        currentLevel = GameManager.Instance.CurrentLevel;
        CreateEnemies();

        LevelManager.OnRoundBegun += OnRoundBegun;
        GameOverManager.OnGameOver += OnGameOver;
    }

    private void OnDisable()
    {
        LevelManager.OnRoundBegun -= OnRoundBegun;
        GameOverManager.OnGameOver -= OnGameOver;
    }

    private void OnGameOver(GameOverEventArgs args)
    {
        StopAllCoroutines();
        ClearAllEnemies();
    }

    private void ClearAllEnemies()
    {
        foreach (List<GameObject> pool in enemyPools.Values)
        {
            pool.ForEach((enemy) =>
            {
                enemy.gameObject.SetActive(false);
            });
        }
    }

    private void OnRoundBegun(OnRoundBegunEventArgs args)
    {
        //spawnDelay = 1.0f / (0.5f + ((args.roundIndex) * SPAWN_DELAY_GROWTH_FACTOR));
        StartCoroutine(EnemySpawnUpdate(args.roundIndex));
    }

    private IEnumerator EnemySpawnUpdate(int roundIndex)
    {
        Debug.Log("Spawning enemies.");
        //yield return new WaitForSeconds(INITIAL_SPAWN_DELAY);

        var currentRound = currentLevel.Rounds[roundIndex];
        foreach(EnemyWave wave in currentRound.Waves)
        {
            for (int i = 0; i < wave.Count; i++)
            {
                var pool = enemyPools[wave.Enemy.ID];
                SpawnEnemy(pool);
                yield return new WaitForSeconds(spawnDelay);
            }
        }
    }

    private void SpawnEnemy(List<GameObject> enemyPool)
    {
        GameObject toSpawn;
        int i = 0;

        do
        {
            toSpawn = enemyPool[i];
            i = (i + 1) % enemyPool.Count;

        } while (toSpawn.activeSelf);

       
        toSpawn.transform.position = SpawnPosition;
        toSpawn.GetComponent<TargetRotation>().RotateTransform.up = Vector3.down;

        toSpawn.gameObject.SetActive(true);
    }

    private Vector3 SpawnPosition
    {
        get
        {
            //float rand = UnityEngine.Random.Range(1, 10);
            //float x = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width * (rand / 10.0f), 0)).x;
            //float y = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y;
            //var spawnPosition = new Vector2(x, y);

            var spawnPosition = UnityEngine.Random.insideUnitCircle * 7.5f;

            return spawnPosition;
        }
    }
}
