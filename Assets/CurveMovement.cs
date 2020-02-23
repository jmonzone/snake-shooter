using UnityEngine;

public class CurveMovement : MonoBehaviour
{
    private float startTime;

    private void OnEnable()
    {
        startTime = Time.time;
    }

    private void Update()
    {
        var direction = new Vector3(Time.time - startTime,0);
        //transform.position += 
    }
}
