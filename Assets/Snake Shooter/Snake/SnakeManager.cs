using System;
using UnityEngine;

public class SnakeManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform snakeHead;
    [SerializeField] private SnakeNode firstSnakeNode;
    [SerializeField] private SnakeNode lastSnakeNode;
    [SerializeField] private TargetFollower snakeTail;

    public static event Action OnLastNodeDetatched;

    private void OnEnable()
    {
        UpgradeDisplay.OnUpgradeSelected += AddTower;
        GameOverManager.OnRevive += OnRevive;
    }

    private void OnDisable()
    {
        UpgradeDisplay.OnUpgradeSelected -= AddTower;
        GameOverManager.OnRevive -= OnRevive;
    }

    private void AddTower(ScriptableTower tower)
    {
        var snakeNode = Instantiate(tower.Prefab, transform.position, Quaternion.identity, transform).GetComponent<SnakeNode>();

        Attach(snakeNode);
        snakeNode.OnEnemyCollision += Detach;
    }

    private void Attach(SnakeNode snakeNode)
    {
        if (!firstSnakeNode)
        {
            firstSnakeNode = snakeNode;
            snakeNode.GetComponent<TargetFollower>().Target = snakeHead;
        }
        else
        {
            snakeNode.Next = lastSnakeNode;
            snakeNode.GetComponent<TargetFollower>().Target = lastSnakeNode.transform;
        }

        snakeNode.gameObject.SetActive(true);
        lastSnakeNode = snakeNode;
        snakeTail.Target = lastSnakeNode.transform;
    }

    private void Detach()
    {
        lastSnakeNode.gameObject.SetActive(false);
        
        if (lastSnakeNode == firstSnakeNode)
        {
            Debug.Log("Last tower has been detatched.");
            OnLastNodeDetatched?.Invoke();
        } else
        {
            lastSnakeNode = lastSnakeNode.Next;

            var lastSnakeNodeFollower = lastSnakeNode.GetComponent<TargetFollower>();
            snakeTail.Target = lastSnakeNode.transform;
            snakeTail.transform.position = lastSnakeNode.transform.position + (lastSnakeNodeFollower.transform.position - lastSnakeNodeFollower.TargetPosition).normalized * 0.5f;
        }

    }

    private void OnRevive()
    {
        foreach (SnakeNode snakeNode in GetComponentsInChildren<SnakeNode>(true))
        {
            snakeNode.gameObject.SetActive(true);

            if (snakeNode == firstSnakeNode) continue;

            Attach(snakeNode);
        }
    }

}
