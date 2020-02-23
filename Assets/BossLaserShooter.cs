using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLaserShooter : MonoBehaviour
{
    private LineRenderer lineRenderer;

    [SerializeField] private Material preLaserMat;
    [SerializeField] private Material laserMat;

    private float laserRange = 20.0f;

    private Transform target;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
    }

    private void OnEnable()
    {
        target = null;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void Update()
    {
        if (lineRenderer.enabled)
        {
            lineRenderer.SetPosition(0, transform.position);
        }
    }

    private void Shoot()
    {
        if (!target) return;

        var direction = target.position - transform.position;
        direction.Normalize();

        lineRenderer.enabled = true;
        lineRenderer.SetPositions(new Vector3[] { transform.position, transform.position + direction * laserRange });

        StartCoroutine(LaserUpdate(transform.position, direction));
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (target) return;

        if (collision.CompareTag(Tags.SNAKE_BODY))
        {
            target = collision.transform;
            Shoot();
        }
        else if (collision.CompareTag(Tags.SNAKE_HEAD))
        {
            target = collision.transform;
            Shoot();
        }
    }

    private IEnumerator LaserUpdate(Vector3 start, Vector3 direction)
    {
        var pre = new Color(150, 0, 0, 25);
        lineRenderer.material = preLaserMat;

        yield return new WaitForSeconds(2.0f);

        lineRenderer.material = laserMat;


        RaycastHit2D[] hits = Physics2D.RaycastAll(start, direction, laserRange);
        Debug.Log(hits);
        if (hits.Length > 0)
        {
            var bodies = new List<GameObject>();
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.CompareTag(Tags.SNAKE_BODY))
                {
                    bodies.Add(hits[i].collider.gameObject);
                }
            }

            bodies.ForEach((body) =>
            {
                var dismantler = body.GetComponentInParent<TowerDismantler>();
                dismantler.Dismantle();
                dismantler.gameObject.SetActive(false);
            });

            if (bodies.Count == 0 && hits[0].collider.CompareTag(Tags.SNAKE_HEAD))
            {
                hits[0].collider.GetComponentInParent<SnakeManager>().HitHead();
            }

        }

        yield return new WaitForSeconds(1.0f);

        lineRenderer.enabled = false;

        yield return new WaitForSeconds(2.0f);
        Shoot();

        target = null;



    }
}
