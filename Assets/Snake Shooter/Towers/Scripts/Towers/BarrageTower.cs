using System.Collections;
using UnityEngine;

public class BarrageTower : ProjectileTower
{
    protected virtual int ShotCount => 3;
    protected virtual float MultiShotDelay => 0.05f;

    protected override IEnumerator ShootUpdate()
    {
        while (gameObject.activeSelf && enabled)
        {
            yield return new WaitUntil(() => Target);
            for (int i = 0; i < ShotCount; i++)
            {
                if (Target && Target.gameObject.activeSelf)
                {
                    Shoot(Target.GetComponent<Enemy>());
                }
                else break;
                yield return new WaitForSeconds(MultiShotDelay);
            }
            Target = null;
            yield return new WaitForSeconds(ShotDelay);
        }
    }
}

