using System;
using UnityEngine;

public class DamageTower : Tower
{
    [Header("Stats")]
    [SerializeField] private float power = 2.0f;

    protected override void OnShootCollision(Transform transform)
    {
        var enemyHealth = transform.gameObject.GetComponent<EnemyHealth>();
        if(enemyHealth) enemyHealth.Value -= power;
    }
}
