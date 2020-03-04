using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooter : Shooter
{
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private float missleSpeed;
    [SerializeField] private int shotCount = 1;
    [SerializeField] private float multiShotDelay = 0.0f;
    [SerializeField] private int projectileLayer;
    [SerializeField] private float initialAttackDelay = 0.0f;

    protected int poolIndex;
    protected List<Projectile> ProjectilePool { get; private set; }

    private void Awake()
    {
        ProjectilePool = new List<Projectile>();
        for (int i = 0; i < 10; i++)
        {
            CreateProjectile();
        }
    }

    private void CreateProjectile()
    {
        var projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectile.gameObject.layer = projectileLayer;
        ProjectilePool.Add(projectile);
        projectile.gameObject.SetActive(false);
    }

    protected override IEnumerator ShootUpdate()
    {
        yield return new WaitForSeconds(initialAttackDelay);

        while(gameObject.activeSelf && enabled)
        {
            yield return new WaitUntil(() => Target);
            yield return new WaitForSeconds(ShootDelay);

            for (int i = 0; i < shotCount; i++)
            {
                Shoot();
                yield return new WaitForSeconds(multiShotDelay);
            }
        }
    }

    protected override void Shoot()
    {
        base.Shoot();

        if (!Target) return;

        var direction = Target.position - ShootPosition;
        direction.Normalize();

        Projectile projectile = ProjectilePool[poolIndex];
        poolIndex = (poolIndex + 1) % ProjectilePool.Count;

        projectile.transform.position = ShootPosition;
        projectile.gameObject.SetActive(true);

        projectile.Shoot(direction * missleSpeed, InvokeShootCollision);
    }
}
