using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Speed speed;
    public Speed Speed => speed;

    protected Health health;
    public Health Health => health;

    protected Status status;
    public Status Status => status;

    public event Action OnEnemyHasReachedBase;

    private void Awake()
    {
        speed = GetComponent<Speed>();
        health = GetComponent<Health>();
        status = GetComponent<Status>();
    }

    protected virtual void Update()
    {
        transform.position += Vector3.down * Time.deltaTime * speed.Value;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Base"))
        {
            gameObject.SetActive(false);
            OnEnemyHasReachedBase?.Invoke();
        }
    }

}
