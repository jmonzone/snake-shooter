using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class FreezeTower : StatusTower
{
    [SerializeField] private float slowValue = 0.1f;

    protected override void OnShootCollision(Transform target)
    {
        base.OnShootCollision(target);
        var enemySpeed = target.GetComponent<EnemySpeed>();
        enemySpeed.Value *= slowValue;

        CoroutineManager.Instance.Delay(Duration, () =>
        {
            enemySpeed.Value /= slowValue;
        });
    }
}
