using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SnakeHead snakeHead;

    private AudioSource audioSource;

    private int currencyCount = 0;

    public int CurrencyCount
    {
        get => currencyCount;
        set
        {
            currencyCount = value;
            PlayerPrefs.SetInt(PlayerPrefsKeys.CURRENCY_KEY, currencyCount);
        }
    }


    private void Awake()
    {
        if (PlayerPrefs.HasKey(PlayerPrefsKeys.CURRENCY_KEY))
            CurrencyCount = PlayerPrefs.GetInt(PlayerPrefsKeys.CURRENCY_KEY);

        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        snakeHead.OnCurrencyPickedUp += IncrementCoinCount;
        snakeHead.OnCurrencyPickedUp += PlayCoinAudio;
    }

    private void IncrementCoinCount()
    {
        CurrencyCount++;
    }

    private void PlayCoinAudio()
    {
        audioSource.Play();
    }
}
