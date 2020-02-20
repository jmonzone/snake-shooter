using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyRotation : MonoBehaviour
{
    private const float ROTATION_SPEED = 100.0f;

    private void Update()
    {
        transform.Rotate(Vector3.up, ROTATION_SPEED * Time.deltaTime);
    }
}
