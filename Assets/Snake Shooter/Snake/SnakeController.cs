using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SnakeHead snakeHead;

    private Vector3 direction = Vector3.up;

    public event Action<Vector3> OnMove;
    public event Action OnMoveBegin;
    public event Action OnMoveEnd;


    public Vector3 Direction
    {
        get => direction;
    }

    private void Start()
    {
        LevelManager.OnLevelBegun += OnLevelBegun;
        LevelManager.OnLevelEnded += OnLevelEnded;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            startPos.z = 0;

            StartCoroutine(MoveUpdate(startPos));
        }

    }

    private IEnumerator MoveUpdate(Vector3 startPos)
    {
        OnMoveBegin?.Invoke();

        while (Input.GetMouseButton(0))
        {
            var endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            endPos.z = 0;

            var direction = endPos - startPos;
            direction *= 0.75f;

            if(direction.magnitude > 1.0f)
                direction = direction.normalized;

            snakeHead.direction = direction;
            OnMove?.Invoke(snakeHead.transform.position + direction);
            yield return new WaitForFixedUpdate();
        }

        snakeHead.direction = Vector3.zero;

        OnMoveEnd?.Invoke();

    }

    private void OnLevelBegun(int level)
    {
        snakeHead.canMove = true;
    }

    private void OnLevelEnded(int level)
    {
        snakeHead.canMove = false;
    }
}
