using System;
using UnityEngine;

public class SnakeManager : MonoBehaviour
{
    private SnakeHead snakeHead;
    private SnakeNode lastSnakeNode;

    public event Action OnHeadDestroyed;

    private void Awake()
    {
        snakeHead = GetComponentInChildren<SnakeHead>();
        lastSnakeNode = snakeHead;
    }

    private void OnEnable()
    {
        snakeHead.OnEnemyCollision += OnHeadCollision;
        GameOverManager.OnRevive += OnRevive;
        UpgradeDisplay.OnUpgradeSelected += AddSnakeTower;
    }

    private void OnDisable()
    {
        snakeHead.OnEnemyCollision -= OnHeadCollision;
        GameOverManager.OnRevive -= OnRevive;
        UpgradeDisplay.OnUpgradeSelected -= AddSnakeTower;
    }


    public void HitHead()
    {
        snakeHead.gameObject.SetActive(false);
        OnHeadDestroyed?.Invoke();
    }
    private void OnHeadCollision()
    {
        if (lastSnakeNode == snakeHead)
        {
            HitHead();
        }
        else
        {
            lastSnakeNode.GetComponent<TowerDismantler>().Dismantle();
        }
    }

    private void AddSnakeTower(Tower snakeTower)
    {
        var snakeFollower = Instantiate(snakeTower, transform.position, Quaternion.identity, transform).GetComponent<SnakeFollower>();
        AttachToSnake(snakeFollower);
        snakeFollower.gameObject.SetActive(true);

        var towerDismantler = snakeFollower.GetComponent<TowerDismantler>();
        towerDismantler.OnRepair += () => OnRepair(snakeFollower);
        towerDismantler.OnDismantle += () => OnDismantle(snakeFollower);

    }

    private void AttachToSnake(SnakeNode snakeNode)
    {
        lastSnakeNode.previous = snakeNode;
        snakeNode.next = lastSnakeNode;
        snakeNode.previous = null;

        var direction = lastSnakeNode.next ? (lastSnakeNode.transform.position - lastSnakeNode.next.transform.position).normalized : (Vector3)UnityEngine.Random.insideUnitCircle;
        snakeNode.transform.position = lastSnakeNode.transform.position + direction * 0.5f;

        lastSnakeNode = snakeNode;
    }

    private void OnDismantle(SnakeFollower snakeFollower)
    {
        if (snakeFollower == lastSnakeNode)
        {
            lastSnakeNode = snakeFollower.next;
            lastSnakeNode.previous = null;
        }
        else if (snakeFollower.previous)
        {
            snakeFollower.next.previous = snakeFollower.previous;
            snakeFollower.previous.next = snakeFollower.next;
        }

        snakeFollower.next = null;
        snakeFollower.previous = null;
    }

    private void OnRepair(SnakeNode snakeNode)
    {
        AttachToSnake(snakeNode);
    }

    private void OnRevive()
    {
        lastSnakeNode = snakeHead;
        snakeHead.gameObject.SetActive(true);

        foreach (SnakeNode snakeNode in transform.GetComponentsInChildren<SnakeNode>(includeInactive: true))
        {
            if (snakeNode == snakeHead) continue;

            snakeNode.gameObject.SetActive(true);
            snakeNode.GetComponent<TowerDismantler>().Repair();
        }
    }

}
