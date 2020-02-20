using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SnakeHead snakeHead;

    private int currencyCount = 0;

    public int CurrencyCount
    {
        get => currencyCount;
        set
        {
            currencyCount = value;
            PlayerPrefs.SetInt(CURRENCY_KEY, currencyCount);
        }
    }

    private const string CURRENCY_KEY = "CURRENCY";

    private void Awake()
    {
        if (PlayerPrefs.HasKey(CURRENCY_KEY))
            CurrencyCount = PlayerPrefs.GetInt(CURRENCY_KEY);
    }

    private void Start()
    {
        snakeHead.OnCurrencyPickedUp += IncrementCoinCount;
    }

    private void IncrementCoinCount()
    {
        CurrencyCount++;
        Debug.Log(currencyCount);
    }
}
