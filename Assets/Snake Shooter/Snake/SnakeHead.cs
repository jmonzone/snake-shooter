using System;
using UnityEngine;

public class SnakeNode : MonoBehaviour
{
    protected const float SPEED = 6.0f;
    public Vector3 direction = Vector3.zero;

    public SnakeNode next;
    public SnakeNode previous;

    protected virtual void Update()
    {
        transform.position += direction * SPEED * Time.deltaTime;
    }
}

public class SnakeHead : SnakeNode
{
    public bool canMove = true;

    public event Action OnEnemyCollision;
    public event Action OnCurrencyPickedUp;

    protected override void Update()
    {
        if (!canMove) return;

        var temp = transform.position + (direction * SPEED * Time.deltaTime);
        if (temp.x < Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x + 0.2f || temp.x > Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x - 0.2f)
        {
            direction.x = 0;
        }
        if (temp.y < Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y + 0.5f || temp.y > Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y - 0.25f)
        {
            direction.y = 0;
        }


        base.Update();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            OnEnemyCollision?.Invoke();
        }

        if (collision.gameObject.CompareTag("Currency"))
        {
            OnCurrencyPickedUp?.Invoke();
        }
    }
}
