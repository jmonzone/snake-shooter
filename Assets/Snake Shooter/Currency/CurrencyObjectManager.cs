using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyObjectManager : MonoBehaviour
{
    [Header("Options")]
    [SerializeField] private Transform currencyCollectionPos;
    [SerializeField] private GameObject currencyPrefab;

    private int poolIndex = 0;
    private List<GameObject> currencyPool;
    private AudioSource audioSource;

    private const int DEFAULT_CURRENCY_POOL_COUNT = 20;

    public static event Action<Transform> OnCurrencyCollectBegin;
    public static event Action OnCurrencyObjectCollected;
    public static event Action OnAllCurrencyObjectsCollected;

    private void OnEnable()
    {
        audioSource = GetComponent<AudioSource>();
        currencyPool = new List<GameObject>();

        for (int i = 0; i < DEFAULT_CURRENCY_POOL_COUNT; i++)
        {
            CreateCurrency();
        }

        EnemyManager.OnEnemyDefeated += OnEnemyDefeated;
        LevelManager.OnRoundEnded += OnRoundEnded;
        GameOverManager.OnGameOver += ClearAllCurrency;
    }

    private void OnDisable()
    {
        EnemyManager.OnEnemyDefeated -= OnEnemyDefeated;
        LevelManager.OnRoundEnded -= OnRoundEnded;
        GameOverManager.OnGameOver -= ClearAllCurrency;
    }

    private void CreateCurrency()
    {
        var currency = Instantiate(currencyPrefab, transform).GetComponent<CurrencyMovement>();
        currencyPool.Add(currency.gameObject);
        currency.OnCollected += InvokeCurrencyCollected;
        currency.gameObject.SetActive(false);
    }

    private void OnEnemyDefeated(GameObject enemy)
    {
        DropCurrency(enemy.transform.position);
    }

    private void InvokeCurrencyCollected()
    {
        GameManager.Instance.CurrencyCount++;
        audioSource.Play();
        OnCurrencyObjectCollected?.Invoke();
    }

    private void OnRoundEnded(int round)
    {
        OnCurrencyCollectBegin?.Invoke(currencyCollectionPos);
        StartCoroutine(CurrencyCollectUpdate());
    }

    private IEnumerator CurrencyCollectUpdate()
    {
        yield return new WaitForSeconds(1.0f);
        OnAllCurrencyObjectsCollected?.Invoke();
    }

    private void DropCurrency(Vector3 spawnPos)
    {
        var currency = currencyPool[poolIndex];
        poolIndex = (poolIndex + 1) % currencyPool.Count;

        currency.gameObject.SetActive(true);
        currency.transform.position = spawnPos;
    }

    private void ClearAllCurrency(GameOverEventArgs args)
    {
        currencyPool.ForEach((currency) =>
        {
            currency.gameObject.SetActive(false);
        });
    }
}
