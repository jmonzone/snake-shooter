using System;
using UnityEngine;

public class SnakeNode : MonoBehaviour
{
    public SnakeNode Next { get; set; }

    public event Action OnEnemyCollision;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Tags.ENEMY) || collision.gameObject.CompareTag(Tags.ENEMY_PROJECTILE))
        {
            OnEnemyCollision?.Invoke();
        }
    }
}
