using UnityEngine;

public class CurveMovement : EnemyMovement
{
    private float Amplitude => speed.Value * 0.5f;
    private const float frequency = 2.0f;

    private float startTime;
    private float sign;

    protected override void OnEnable()
    {
        base.OnEnable();
        startTime = Time.time;
        sign = Random.Range(0, 2) * 2 - 1;
    }

    protected override void Update()
    {
        base.Update();

        if (!Target) return;

        var c = sign * Amplitude * Mathf.Sin((Time.time - startTime) * frequency);

        var v1 = Vector3.forward;
        var v2 = Target.position - transform.position;
        var v3 = Vector3.Cross(v1, v2);

        rb.velocity += (Vector2)v3 * c;
    }
}
