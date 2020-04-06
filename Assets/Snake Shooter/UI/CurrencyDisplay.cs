using UnityEngine;
using UnityEngine.UI;

public class CurrencyDisplay : MonoBehaviour
{
    private Text text;

    private void OnEnable()
    {
        text = GetComponent<Text>();
        UpdateDisplay(GameManager.Instance.CurrencyCount);
        GameManager.Instance.OnCurrencyCountChanged += UpdateDisplay;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnCurrencyCountChanged -= UpdateDisplay;
    }

    private void UpdateDisplay(int count)
    {
        text.text = count.ToString();
    }
}
