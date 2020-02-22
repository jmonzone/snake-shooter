using UnityEngine;

public class SnakeFollower : SnakeNode
{
    private const float FOLLOW_DISTANCE = 0.35f;

    private TowerDismantler towerDismantler;

    protected virtual void Awake()
    {
        towerDismantler = GetComponent<TowerDismantler>();
    } 

    protected override void Update()
    {
        if (towerDismantler.IsDismantled) return;

        var distance = Vector3.Distance(next.transform.position, transform.position);

        direction = next.transform.position - transform.position;
        direction.Normalize();

        if (distance > FOLLOW_DISTANCE)
        {
            base.Update();
        }
    }
}
