using System;
using UnityEngine;

public class CurrencyMovement : MonoBehaviour
{
    private Transform target;
    private const float SPEED = 7.5f;

    public event Action OnCollected;

    private void OnEnable()
    {
        CurrencyObjectManager.OnCurrencyCollectBegin += OnCurrencyCollectBegin;
    }

    private void OnDisable()
    {
        target = null;
        CurrencyObjectManager.OnCurrencyCollectBegin -= OnCurrencyCollectBegin;
    }

    private void OnCurrencyCollectBegin(Transform currencyCollectionPos)
    {
        target = currencyCollectionPos;
    }

    private void Update()
    {
        if (!target) return;
        var direction = (target.position - transform.position).normalized;
        transform.position += direction * SPEED * Time.deltaTime;

        if(Vector3.Distance(target.position, transform.position) < 0.5f)
        {
            gameObject.SetActive(false);
            OnCollected?.Invoke();
        }
    }
}
