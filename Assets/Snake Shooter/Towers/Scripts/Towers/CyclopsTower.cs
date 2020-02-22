using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyclopsTower : LaserTower
{
    protected override float Power => 2.0f;
    protected override float Range => 2.0f;
    protected override float AttackSpeed => 1.0f;

    private float laserRange = 8.0f;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag(Tags.ENEMY))
        {
            if (!Target)
            {
                Target = collision.transform;
            }
            else if (collision.transform != Target)
            {
                var targetDistance = Vector3.Distance(Target.position, transform.position);
                var collisionDistance = Vector3.Distance(collision.transform.position, transform.position);

                if (collisionDistance < targetDistance)
                {
                    Target = collision.transform;
                }
            }
        }
    }

    protected override void Shoot(Enemy enemy)
    {
        base.Shoot(enemy);

        var direction = enemy.transform.position - transform.position;
        direction.Normalize();

        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, direction, laserRange);

        if (hits.Length > 0)
        {
            var enemies = new List<Enemy>();
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.CompareTag(Tags.ENEMY))
                {
                    enemies.Add(hits[i].collider.GetComponent<Enemy>());
                }
            }

            enemies.ForEach((x) =>
            {
                x.Health.Value -= Power;
            });
        }

        lineRenderer.enabled = true;
        lineRenderer.SetPositions(new Vector3[] { transform.position, transform.position + direction * laserRange });

        Invoke(nameof(DisableLineRenderer), 0.05f);
    }

    private void DisableLineRenderer()
    {
        lineRenderer.enabled = false;

    }
}
