using System.Collections.Generic;
using UnityEngine;

public class BlazeTower : ProjectileTower
{
    public override string Description => "Blaze Tower";

    //protected override float Range => 3.0f;
    protected override float AttackSpeed => 0.6f;
    protected override float MissleSpeed => 100.0f;

    private readonly float burnValue = 4.5f;
    private readonly float burnDuration = 5.0f;

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

    protected override void OnEnemyHit(Enemy enemy)
    {
        var particle = particlePool[particlePoolIndex];
        particlePoolIndex = (particlePoolIndex + 1) % particlePool.Count;
        particle.gameObject.SetActive(true);

        particle.GetComponent<TargetFollower>().target = enemy.transform;

        var burnEffect = new BurnEffect(burnValue, burnDuration);
        enemy.Status.Add(burnEffect, () => {
            if (hitEnemies.Contains(enemy)) hitEnemies.Remove(enemy);
            if (particle) particle.gameObject.SetActive(false);
        });

        hitEnemies.Add(enemy);
    }
}
