using System.Collections;
using UnityEngine;

public class SprayTower : ProjectileTower
{
    public override string Description => "Spray Tower";
    //protected override float Power => base.Power * 0.75f;
    protected override float AttackSpeed => base.AttackSpeed;

    protected override IEnumerator ShootUpdate()
    {
        while (gameObject.activeSelf)
        {
            yield return new WaitUntil(() => Target);
            if (Target && Target.gameObject.activeSelf)
            {
                Shoot(Target.GetComponent<Enemy>());
                var directions = GetSpreadDirection(Target.transform.position);
                SpreadShot(directions[0]);
                SpreadShot(directions[1]);
                Target = null;
            }
            yield return new WaitForSeconds(ShotDelay);
        }
    }

    protected Vector3[] GetSpreadDirection(Vector3 centerPosition)
    {
        var retVal = new Vector3[2];

        var direction = centerPosition - transform.position;
        direction.Normalize();

        retVal[0] = direction * Mathf.Sin(Mathf.Rad2Deg * 0.3f);
        retVal[1] = direction * Mathf.Sin(-Mathf.Rad2Deg * 0.3f);

        return retVal;


    }

    protected void SpreadShot(Vector3 direction)
    {
        Projectile  projectile = ProjectilePool[poolIndex];
        poolIndex = (poolIndex + 1) % ProjectilePool.Count;

        projectile.gameObject.SetActive(true);
        projectile.transform.position = transform.position;

        direction.Normalize();

        //projectile.Shoot(direction, MissleSpeed, OnEnemyHit);
    }
}

