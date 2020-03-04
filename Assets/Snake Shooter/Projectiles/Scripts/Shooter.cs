using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Targeter))]
public abstract class Shooter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform shootPosition;
    protected Vector3 ShootPosition => shootPosition.position;

    [Header("Options")]
    [SerializeField] private float attackSpeed = 0.25f;
    protected float ShootDelay => 1 / attackSpeed + UnityEngine.Random.Range(-0.15f, 0.15f);

    private Targeter targeter;
    protected Transform Target => targeter.Target;

    private AudioSource audioSource;

    public event Action<Transform> OnShootCollision;

    protected void InvokeShootCollision(Transform transform)
    {
        OnShootCollision?.Invoke(transform);
    }

    private void OnEnable()
    {
        targeter = GetComponent<Targeter>();
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(ShootUpdate());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    protected abstract IEnumerator ShootUpdate();

    protected virtual void Shoot()
    {
        if (!Target) return;

        audioSource.Play(0);
    }
}
