using UnityEngine;

[RequireComponent(typeof(Targeter))]
public class TargetRotation : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform rotateTransform;
    public Transform RotateTransform => rotateTransform;

    [Header("Options")]
    [SerializeField] private float rotationSpeed = 5.0f;

    private Targeter targeter;
    private Transform Target => targeter.Target;

    private void Awake()
    {
        targeter = GetComponent<Targeter>();

        if (!rotateTransform)
            rotateTransform = transform;
    }

    private void Update()
    {
        if (!Target) return;

        var a = rotateTransform.up;
        var b = Target.position - rotateTransform.position;
        var t = Time.deltaTime / (Target.position - rotateTransform.position).magnitude;

        rotateTransform.up = Vector3.Lerp(a, b, t * rotationSpeed);
    }
}
