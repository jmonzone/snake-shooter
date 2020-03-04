using UnityEngine;

public class TargetFollower : ITargeter
{
    [Header("Options")]
    [SerializeField] private float speed = 4.0f;
    [SerializeField] private float followDistance = 0.5f;
    [SerializeField] private bool rotate = false;
    [SerializeField] private Transform rotateTransform;
    [SerializeField] private float rotationSpeed = 5.0f;

    public Vector3 TargetPosition => Target.position;

    private void Update()
    {
        if (!Target) return;

        var direction = Target.position - transform.position;
        direction.Normalize();

        var distance = Vector3.Distance(Target.transform.position, transform.position);

        if (distance > followDistance)
        {
            transform.position += direction * speed * Time.deltaTime;
        }

        if (rotate)
        {
            var a = rotateTransform.up;
            var b = TargetPosition - rotateTransform.position;
            var t = Time.deltaTime / (TargetPosition - rotateTransform.position).magnitude;

            rotateTransform.up = Vector3.Lerp(a, b, t * rotationSpeed);
        }
    }
}
