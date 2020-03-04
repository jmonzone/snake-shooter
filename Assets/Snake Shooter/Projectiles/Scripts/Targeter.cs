using System.Collections.Generic;
using UnityEngine;

public abstract class ITargeter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform target;
    public Transform Target
    {
        get => target;
        set
        {
            target = value;
        }
    }
}

public class Targeter : MonoBehaviour
{
    public Transform Target { get; private set; }

    [Header("Options")]
    [SerializeField] private List<string> targetTags;

    private void OnTriggerStay2D(Collider2D collision)
    {
        foreach (string tag in targetTags)
        {
            if (collision.CompareTag(tag))
            {
                if (!Target)
                {
                    Target = collision.transform;
                }
                else
                {
                    var targetDistance = Vector3.Distance(Target.position, transform.position);
                    var collisionDistance = Vector3.Distance(collision.transform.position, transform.position);

                    if (collisionDistance < targetDistance)
                    {
                        Target = collision.transform;
                    }
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform == Target) Target = null;
    }
}
