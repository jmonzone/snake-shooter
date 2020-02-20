using System.Collections;
using UnityEngine;

public abstract class ShooterTower : Tower
{
    protected virtual float AttackSpeed => 1.0f;
    protected float ShotDelay => 1 / AttackSpeed;

    protected const int INITIAL_POOL_COUNT = 10;

    protected Transform Target { get; set; }

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

    protected abstract void Shoot(Enemy enemy);
    protected virtual void OnEnemyHit(Enemy enemy) { }

}
