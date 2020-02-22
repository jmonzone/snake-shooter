using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyMagnet : MonoBehaviour
{
    private Transform target;
    private const float SPEED = 3.0f;


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag(Tags.SNAKE_HEAD))
        {
            target = collision.transform;
        }
    }

    private void Update()
    {
        if (!target) return;
        var direction = (target.position - transform.position).normalized;
        transform.position += direction * SPEED * Time.deltaTime;
    }
}
