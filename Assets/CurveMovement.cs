using UnityEngine;

public class CurveMovement : MonoBehaviour
{
    private const float amplitude = 1.0f;
    private const float frequency = 2.0f;

    private float startTime;
    private float sign;

    private void OnEnable()
    {
        startTime = Time.time;
        sign = Random.Range(0, 2) * 2 - 1;
    }

    private void Update()
    {
        var x = sign * amplitude * Mathf.Sin((Time.time - startTime) * frequency);
        var direction = new Vector3(x, 0);
        transform.position += direction * Time.deltaTime;
    }
}
