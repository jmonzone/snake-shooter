using UnityEngine;

public abstract class LaserTower : ShooterTower
{
    protected LineRenderer lineRenderer;

    protected override void Awake()
    {
        base.Awake();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
    }
}
