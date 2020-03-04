using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatusTower : Tower
{
    [Header("Options")]
    [SerializeField] private GameObject particlePrefab;
    [SerializeField] private float duration = 3.0f;
    protected float Duration => duration;

    private readonly List<GameObject> particlePool = new List<GameObject>();

    protected override void Awake()
    {
        base.Awake();
        CreateParticles();
    }

    private void CreateParticles()
    {
        for(int i = 0; i < 10; i++)
        {
            var particle = Instantiate(particlePrefab, transform);
            particle.gameObject.GetComponent<ParticleSystem>().Play();
            particle.gameObject.SetActive(false);
            particlePool.Add(particle);
        }
    }

    protected GameObject GetAvailableParticle
    {
        get
        {
            int i = 0;
            GameObject particle = particlePool[i];
            while(particle.activeSelf == true)
            {
                i = (i + 1) % particlePool.Count;
                particle = particlePool[i];
            }
            particle.gameObject.SetActive(true);

            return particle;
        }
    }

    protected override void OnShootCollision(Transform target)
    {
        base.OnShootCollision(target);

        var particle = GetAvailableParticle;
        particle.transform.SetParent(target);
        particle.transform.position = target.position;
        particle.transform.localScale = Vector3.one;
        particle.transform.rotation = Quaternion.Euler(-90, 0, 0);

        if (!gameObject.activeSelf) return;
        CoroutineManager.Instance.Delay(duration, () =>
        {
            particle.gameObject.SetActive(false);
        });
    }
} 

public class BlazeTower : StatusTower
{
    [SerializeField] private float burnDamage = 7.5f;

    protected override void OnShootCollision(Transform target)
    {
        base.OnShootCollision(target);
        var enemyHealth = target.GetComponent<EnemyHealth>();
        CoroutineManager.Instance.Tick(Duration, () =>
        {
            enemyHealth.Value -= burnDamage;
        });
    }
}