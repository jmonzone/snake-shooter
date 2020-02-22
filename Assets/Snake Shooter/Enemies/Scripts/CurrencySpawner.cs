using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencySpawner : MonoBehaviour
{
    [Header("Options")]
    [SerializeField] private GameObject currencyPrefab;

    private int poolIndex = 0;
    private List<GameObject> currencyPool;

    private void Awake()
    {
        currencyPool = new List<GameObject>();
        for(int i = 0; i < 20; i++)
        {
            CreateCurrency();
        }
    }

    private void Start()
    {
        EnemyManager.OnEnemyDefeated += DropCurrency;
    }

    private void CreateCurrency()
    {
        var currency = Instantiate(currencyPrefab, transform);
        currency.gameObject.SetActive(false);

        currencyPool.Add(currency);
    }


    private void DropCurrency(Enemy enemy)
    {
        var currency = currencyPool[poolIndex];
        poolIndex = (poolIndex + 1) % currencyPool.Count;

        currency.gameObject.SetActive(true);
        currency.transform.position = enemy.transform.position;
    }
}
