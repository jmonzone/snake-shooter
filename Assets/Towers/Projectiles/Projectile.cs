using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rb;

    public event Action<Enemy> onCollision;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Shoot(Vector2 force)
    {
        rb.AddForce(force);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            var enemy = collision.gameObject.GetComponent<Enemy>();

            onCollision?.Invoke(enemy);

            gameObject.SetActive(false);
        }
    }
}
