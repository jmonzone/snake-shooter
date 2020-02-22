using System.Collections;
using UnityEngine;

public class SniperTower : ProjectileTower
{
    protected override float Range => 10.0f;
    protected override float Power => 5.0f;
    protected override float AttackSpeed => 0.2f;
    protected override float MissleSpeed => 1100.0f;

    protected override void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (!Target || !Target.gameObject.activeSelf)
            {
                Target = collision.transform;
            }
            else if (collision.transform != Target)
            {
                if (collision.transform.position.y < Target.position.y)
                {
                    Target = collision.transform;
                }
            }
        }
    }
}
