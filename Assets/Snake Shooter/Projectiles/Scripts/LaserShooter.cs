using System.Collections;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LaserShooter : Shooter
{
    [SerializeField] private float laserRange = 8.0f;
    [SerializeField] private bool pierce = false;

    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
    }

    protected override IEnumerator ShootUpdate()
    {
        while (gameObject.activeSelf && enabled)
        {
            yield return new WaitUntil(() => Target);

            Shoot();

            yield return new WaitForSeconds(ShootDelay);
        }
    }

    protected override void Shoot()
    {
        base.Shoot();
        if (!Target) return;

        if (pierce)
        {
            var direction = Target.position - ShootPosition;
            direction.Normalize();

            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, direction, laserRange);

            for (int i = 0; i < hits.Length; i++)
            {
                InvokeShootCollision(hits[i].transform);
            }

            lineRenderer.enabled = true;
            lineRenderer.SetPositions(new Vector3[] { ShootPosition, ShootPosition + direction * laserRange });
        } else
        {
            lineRenderer.enabled = true;
            lineRenderer.SetPositions(new Vector3[] { ShootPosition, Target.position });

            InvokeShootCollision(Target);
        }

        Invoke(nameof(DisableLineRenderer), 0.05f);
    }

    private void DisableLineRenderer()
    {
        lineRenderer.enabled = false;

    }

}
