using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopCurrencyManager : MonoBehaviour
{
    public static event Action<int> OnCurrencyCountChanged;

    private int currencyCount = 0;

    private void Awake()
    {
        if (PlayerPrefs.HasKey(PlayerPrefsKeys.CURRENCY_KEY))
            CurrencyCount = PlayerPrefs.GetInt(PlayerPrefsKeys.CURRENCY_KEY);

        ShopItemManager.OnPurchaseButtonClicked += AddTower;
    }

    private void AddTower(UnlockableTower unlockableTower)
    {
        if (CurrencyCount >= unlockableTower.Price)
        {
            CurrencyCount -= unlockableTower.Price;
            PlayerPrefs.SetInt(unlockableTower.Key, 1);
        }

    }

    public int CurrencyCount
    {
        get => currencyCount;
        set
        {
            currencyCount = value;
            PlayerPrefs.SetInt(PlayerPrefsKeys.CURRENCY_KEY, currencyCount);

            OnCurrencyCountChanged?.Invoke(currencyCount);
        }
    }
}
