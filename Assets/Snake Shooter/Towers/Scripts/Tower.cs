using UnityEngine;

[RequireComponent(typeof(Shooter))]
public class Tower : MonoBehaviour
{
    protected virtual void Awake()
    {
        var shooter = GetComponent<Shooter>();
        shooter.OnShootCollision += OnShootCollision;
    }

    protected virtual void OnShootCollision(Transform target) { }
}
