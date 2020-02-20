using System;
using System.Collections.Generic;
using UnityEngine;

public class SnakeManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameOverManager gameOverManager;

    private SnakeHead snakeHead;
    private SnakeNode lastSnakeNode;

    public event Action OnHeadDestroyed;

    private void Awake()
    {
        snakeHead = GetComponentInChildren<SnakeHead>();
        lastSnakeNode = snakeHead;
    }

    private void Start()
    {
        snakeHead.OnEnemyCollision += OnHeadCollision;
        gameOverManager.OnRevive += OnRevive;
    }

    private void OnHeadCollision()
    {
        if (lastSnakeNode == snakeHead)
        {
            snakeHead.gameObject.SetActive(false);
            OnHeadDestroyed?.Invoke();
        }
        else
        {
            lastSnakeNode.GetComponent<TowerDismantler>().Dismantle();
        }
    }

    public void AddSnakeTower(Tower snakeTower)
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
        snakeNode.transform.position = lastSnakeNode.transform.position;

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
