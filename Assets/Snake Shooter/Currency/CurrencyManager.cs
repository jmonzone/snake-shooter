using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance { get; private set; }

    public int CurrencyCount { get; private set; }

    private const int INITIAL_CURRENCY = 200;
    private int currencyPerRound = INITIAL_CURRENCY;

    private void OnEnable()
    {
        Instance = this;

        CurrencyCount = INITIAL_CURRENCY;

        CurrencyObjectManager.OnCurrencyObjectCollected += OnCurrencyObjectCollected;
        CurrencyObjectManager.OnAllCurrencyObjectsCollected += OnAllCurrencyObjectsCollected;
        UpgradeDisplay.OnUpgradeSelected += OnUpgradeSelected;
    }

    private void OnDisable()
    {
        Instance = null;

        CurrencyObjectManager.OnCurrencyObjectCollected -= OnCurrencyObjectCollected;
        CurrencyObjectManager.OnAllCurrencyObjectsCollected += OnAllCurrencyObjectsCollected;
        UpgradeDisplay.OnUpgradeSelected -= OnUpgradeSelected;
    }

    private void OnCurrencyObjectCollected()
    {
        CurrencyCount++;
    }

    private void OnAllCurrencyObjectsCollected()
    {
        CurrencyCount += currencyPerRound;
        currencyPerRound++;
    }

    private void OnUpgradeSelected(ScriptableTower tower)
    {
        CurrencyCount -= tower.Price;
    }
}
