using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rb;
    private Action<Transform> onCollision;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Shoot(Vector2 velocity, Action<Transform> onCollision)
    {
        this.onCollision = onCollision;
        rb.velocity = velocity;
        transform.up = velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        onCollision?.Invoke(collision.transform);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        onCollision?.Invoke(collision.transform);
    }
}
