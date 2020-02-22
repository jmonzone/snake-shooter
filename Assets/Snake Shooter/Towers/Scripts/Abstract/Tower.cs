using System;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    protected virtual float Range => 3.0f;

    private TowerDismantler towerDismantler;

    //TEMP
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
