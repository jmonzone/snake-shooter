using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineManager : MonoBehaviour
{
    public static CoroutineManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance)
            Destroy(gameObject);
        else
            Instance = this;
    }

    public void Delay(float delay, Action callback)
    {
        StartCoroutine(DelayUpdate(callback, delay));
    }

    private IEnumerator DelayUpdate(Action callback, float delay)
    {
        yield return new WaitForSeconds(delay);
        callback.Invoke();

    }

    public void Tick(float duration, Action callback)
    {
        StartCoroutine(TickUpdate(callback, duration));
    }

    private IEnumerator TickUpdate(Action callback, float duration)
    {
        for( int i = 0; i < duration; i++)
        {
            yield return new WaitForSeconds(1.0f);
            callback?.Invoke();
        }
    }
}
