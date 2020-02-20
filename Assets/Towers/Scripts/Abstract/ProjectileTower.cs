using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTower : ShooterTower
{
    [Header("Options")]
    [SerializeField] private Projectile projectilePrefab;

    protected virtual float MissleSpeed => 300.0f;
    protected virtual float Power => 2.0f;

    protected AudioSource audioSource;

    protected int poolIndex;

    protected List<Projectile> ProjectilePool { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        audioSource = GetComponent<AudioSource>();
    }

    protected override void Start()
    {
        base.Start();

        ProjectilePool = new List<Projectile>();
        for(int i = 0; i < INITIAL_POOL_COUNT; i++)
        {
            SpawnProjectile();
        }
    }

    private void SpawnProjectile()
    {
        var projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        ProjectilePool.Add(projectile);
        projectile.gameObject.SetActive(false);

    }

    protected virtual void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
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

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (Target == collision.transform)
            {
                Target = null;
            }
        }
    }

    protected override void Shoot(Enemy enemy)
    {
        if (!enemy) return;

        Projectile projectile = ProjectilePool[poolIndex];
        poolIndex = (poolIndex + 1) % ProjectilePool.Count;

        projectile.gameObject.SetActive(true);

        var direction = enemy.transform.position - transform.position;
        direction.Normalize();

        projectile.transform.position = transform.position;
        var force = direction * MissleSpeed;

        projectile.Shoot(force);
        projectile.onCollision += OnEnemyHit;

        audioSource.Play();
    }

    protected override void OnEnemyHit(Enemy enemy)
    {
        enemy.Health.Value -= Power;
    }

   

}
