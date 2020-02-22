using UnityEngine;
using UnityEngine.UI;

public class PlayerPrefsKeys
{
    public const string CURRENCY_KEY = "CURRENCY";

    public const string CYCLOPS_TOWER = "CYCLOPS_TOWER";
}

public class CurrencyDisplay : MonoBehaviour
{
    private Text text;

    private void Awake()
    {
        text = GetComponent<Text>();
        UpdateDisplay(PlayerPrefs.GetInt(PlayerPrefsKeys.CURRENCY_KEY));
    }

    private void OnEnable()
    {
        ShopCurrencyManager.OnCurrencyCountChanged += UpdateDisplay;
    }

    private void OnDisable()
    {
        ShopCurrencyManager.OnCurrencyCountChanged -= UpdateDisplay;

    }

    private void UpdateDisplay(int count)
    {
        text.text = count.ToString() ;

    }
}
