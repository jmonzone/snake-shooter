using UnityEngine;

public abstract class LaserTower : ShooterTower
{
    [Header("Options")]
    [SerializeField] protected LineRenderer lineRenderer;

    protected override void Awake()
    {
        base.Awake();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
    }
}
