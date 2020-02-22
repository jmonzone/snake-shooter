using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectRotater : MonoBehaviour
{
    public enum Axis
    {
        X, Y, Z
    }

    [Header("Options")]
    [SerializeField] private Axis axis;
    [SerializeField] private float speed;

    private Vector3 rotationVector;

    private void Awake()
    {
        switch (axis)
        {
            case Axis.X:
                rotationVector = Vector3.right;
                break;
            case Axis.Y:
                rotationVector = Vector3.up;
                break;
            case Axis.Z:
                rotationVector = Vector3.forward;
                break;
        }
    }

    private void Update()
    {
        transform.Rotate(rotationVector, speed * Time.deltaTime);
    }
}
