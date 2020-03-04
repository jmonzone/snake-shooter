using UnityEngine;

[RequireComponent(typeof(Targeter))]
[RequireComponent(typeof(EnemySpeed))]
public class EnemyMovement : MonoBehaviour
{
    protected EnemySpeed speed;
    private Targeter targeter;
    protected Transform Target => targeter.Target;
    protected Rigidbody2D rb;

    private void Awake()
    {
        targeter = GetComponent<Targeter>();
        speed = GetComponent<EnemySpeed>();
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void OnEnable() { }

    protected virtual void Update()
    {
        if (!Target) return;

        var direction = Target.position - transform.position;
        direction.Normalize();

        rb.velocity = direction * speed.Value;
    }

}
