using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerRange : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CircleCollider2D rangeTrigger;

    [Header("Options")]
    [SerializeField] private float range = 3.0f;

    private void Awake()
    {
        rangeTrigger.radius = range;
    }
}
