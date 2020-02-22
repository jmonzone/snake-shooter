using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDismantler : MonoBehaviour
{
    private const float DISMANTLE_FORCE = 50.0f;
    private const float MAX_DISMANTLE_TIME = 5.0f;

    private float timeOfDismantle = 0.0f;
    public bool IsDismantled { get; private set; } = false;

    private Rigidbody2D rb;
    private SnakeNode snakeNode;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    public event Action OnDismantle;
    public event Action OnRepair;


    private void Awake()
    {
        rb = GetComponentInChildren<Rigidbody2D>();
        snakeNode = GetComponent<SnakeNode>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    private void Update()
    {
        if (IsDismantled)
        {
            var timeSinceDismantle = Time.time - timeOfDismantle;

            spriteRenderer.color = Color.Lerp(originalColor, Color.gray, timeSinceDismantle / MAX_DISMANTLE_TIME);

            if (timeSinceDismantle > MAX_DISMANTLE_TIME)
                Dissapear();
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Dismantle();
        }
        else if (IsDismantled && collision.gameObject.CompareTag("Snake Head"))
        {
            Repair();
        }
    }

    public void Dismantle()
    {
        IsDismantled = true;
        timeOfDismantle = Time.time;

        var direction = UnityEngine.Random.insideUnitCircle;
        rb.constraints = RigidbodyConstraints2D.None;
        rb.drag = 5.0f;
        rb.AddForce(direction * 50.0f);

        OnDismantle?.Invoke();
    }

    private void Dissapear()
    {
        gameObject.SetActive(false);
    }

    public void Repair()
    {
        IsDismantled = false;

        spriteRenderer.color = originalColor;

        rb.constraints = RigidbodyConstraints2D.FreezePosition;
        rb.drag = 0.0f;

        OnRepair?.Invoke();
    }

}
