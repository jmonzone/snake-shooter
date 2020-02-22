using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Currency
{
    public static event Action<int> OnCurrencyCountChanged;

    public static int Count
    {
        get => PlayerPrefs.GetInt(PlayerPrefsKeys.CURRENCY_KEY, 0);
        set
        {
            PlayerPrefs.SetInt(PlayerPrefsKeys.CURRENCY_KEY, value);
            OnCurrencyCountChanged?.Invoke(value);
        }
    }
}
