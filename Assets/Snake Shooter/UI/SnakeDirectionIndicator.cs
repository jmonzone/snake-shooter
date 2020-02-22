using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeDirectionIndicator : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SnakeController snakeController;

    private void Start()
    {
        snakeController.OnMove += OnMove;

        snakeController.OnMoveBegin += OnMoveBegin;
        snakeController.OnMoveEnd += OnMoveEnd;
        gameObject.SetActive(false);
    }

    private void OnMove(Vector3 position)
    {
        transform.position = position;
    }

    private void OnMoveBegin()
    {
        gameObject.SetActive(true);
    }

    private void OnMoveEnd()
    {
        gameObject.SetActive(false);
    }
}
