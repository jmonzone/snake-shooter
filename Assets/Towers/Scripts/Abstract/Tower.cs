using System;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    public virtual string Description => "Snake Tower";
    protected virtual float Range => 2.0f;

    private TowerDismantler towerDismantler;

    public Sprite Sprite => GetComponentInChildren<SpriteRenderer>().sprite;
    public Color Color => GetComponentInChildren<SpriteRenderer>().color;

    protected virtual void Awake()
    {
        towerDismantler = GetComponent<TowerDismantler>();
    }

    protected virtual void OnEnable() { }

    protected virtual void Start()
    {
        var col = GetComponentInChildren<CircleCollider2D>();
        col.radius = Range;

        if (towerDismantler)
        {
            towerDismantler.OnDismantle += () => enabled = false;
            towerDismantler.OnRepair += () => enabled = true;
        }
    }
}
