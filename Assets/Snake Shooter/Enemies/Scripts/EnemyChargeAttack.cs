using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Targeter))]
public class EnemyChargeAttack : MonoBehaviour
{
    [Header("Options")]
    [SerializeField] private float startChargeDistance = 2.5f;
    [SerializeField] private float windUpLength = 1.0f;
    [SerializeField] private float windBackSpeed = 0.25f;
    [SerializeField] private float chargeLength = 0.5f;
    [SerializeField] private float chargeSpeed = 2.0f;
    [SerializeField] private float cooldown = 3.0f;

    private bool canCharge = true;
    private Transform Target => targeter.Target;

    private Targeter targeter;
    private TargetRotation targetRotation;
    private EnemyMovement enemyMovement;
    private Rigidbody2D rb;

    private void Awake()
    {
        targeter = GetComponent<Targeter>();
        targetRotation = GetComponent<TargetRotation>();
        enemyMovement = GetComponent<EnemyMovement>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!Target) return;
        if (!canCharge) return;

        if (Vector3.Distance(transform.position, Target.position) < startChargeDistance)
        {
            var direction = Target.position - transform.position;
            StartCoroutine(ChargeUpdate(direction));
        }
    }

    private void OnDisable()
    {
        enemyMovement.enabled = targetRotation.enabled = true;
        canCharge = true;
    }

    private IEnumerator ChargeUpdate(Vector3 direction)
    {
        enemyMovement.enabled = targetRotation.enabled = false;
        canCharge = false;

        var startTime = Time.time;
        while (Time.time - startTime < windUpLength)
        {
            rb.velocity = -1 * direction * windBackSpeed;
            yield return new WaitForFixedUpdate();
        }

        startTime = Time.time;
        while (Time.time - startTime < chargeLength)
        {
            rb.velocity = direction * chargeSpeed;
            yield return new WaitForFixedUpdate();
        }

        enemyMovement.enabled = targetRotation.enabled = true;

        yield return new WaitForSeconds(cooldown);
        canCharge = true;
    }
}
