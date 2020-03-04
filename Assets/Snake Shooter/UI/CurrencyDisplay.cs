using UnityEngine;
using UnityEngine.UI;

public class CurrencyDisplay : MonoBehaviour
{
    private Text text;

    private void OnEnable()
    {
        text = GetComponent<Text>();
    }

    private void Update()
    {
        text.text = CurrencyManager.Instance.CurrencyCount.ToString() ;
    }
}
