using System.Collections;
using UnityEngine;

public abstract class ShooterTower : Tower
{
    protected virtual float AttackSpeed => 1.0f;
    protected float ShotDelay => 1 / AttackSpeed;
    protected virtual float Power => 1.5f;

    protected const int INITIAL_POOL_COUNT = 10;

    protected Transform Target { get; set; }

    protected AudioSource audioSource;

    protected override void Awake()
    {
        base.Awake();
        audioSource = GetComponent<AudioSource>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(ShootUpdate());
    }

    protected virtual IEnumerator ShootUpdate()
    {
        while (gameObject.activeSelf && enabled)
        {
            yield return new WaitUntil(() => Target);
            if (Target && Target.gameObject.activeSelf)
            {
                Shoot(Target.GetComponent<Enemy>());
                Target = null;
            }
            yield return new WaitForSeconds(ShotDelay);
        }
    }

    protected virtual void Shoot(Enemy enemy)
    {
        audioSource.Play();
    }
    protected virtual void OnEnemyHit(Enemy enemy) { }

}
