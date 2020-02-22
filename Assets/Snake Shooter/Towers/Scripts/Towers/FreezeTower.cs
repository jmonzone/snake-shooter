using System.Collections.Generic;
using UnityEngine;

public class FreezeTower : LaserTower
{
    //protected override float Range => 2.0f;
    protected override float AttackSpeed => 0.70f;

    private readonly float slowValue = 0.25f;
    private readonly float slowDuration = 3.0f;

    private readonly List<Enemy> hitEnemies = new List<Enemy>();

    [SerializeField] private GameObject particlePrefab;
    private int particlePoolIndex;
    private List<GameObject> particlePool;

    protected override void Awake()
    {
        base.Awake();
        SpawnParticles();
    }

    private void SpawnParticles()
    {
        particlePool = new List<GameObject>();
        for (int i = 0; i < INITIAL_POOL_COUNT; i++)
        {
            SpawnParticle();
        }
    }

    private void SpawnParticle()
    {
        var particle = Instantiate(particlePrefab, transform.position, Quaternion.identity);
        particlePool.Add(particle);
        particle.SetActive(false);

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            var enemy = collision.GetComponent<Enemy>();
            if (hitEnemies.Contains(enemy)) return;

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

        lineRenderer.enabled = true;
        lineRenderer.SetPositions(new Vector3[] { transform.position, enemy.transform.position });
        Invoke(nameof(DisableLineRenderer), 0.05f);

        var particle = particlePool[particlePoolIndex];
        particlePoolIndex = (particlePoolIndex + 1) % particlePool.Count;
        particle.gameObject.SetActive(true);

        particle.GetComponent<TargetFollower>().target = enemy.transform;

        var freezeEffect = new FreezeEffect(slowValue, slowDuration);
        enemy.Status.Add(freezeEffect, () => {
            if (hitEnemies.Contains(enemy)) hitEnemies.Remove(enemy);
            if (particle) particle.gameObject.SetActive(false);
        });

        hitEnemies.Add(enemy);
    }

    private void DisableLineRenderer()
    {
        lineRenderer.enabled = false;

    }
}
