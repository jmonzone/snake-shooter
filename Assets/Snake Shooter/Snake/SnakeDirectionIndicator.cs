using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeDirectionIndicator : MonoBehaviour
{
    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        SnakeController.OnMove += OnMove;
        SnakeController.OnMoveBegin += OnMoveBegin;
        SnakeController.OnMoveEnd += OnMoveEnd;
    }

    private void OnDisable()
    {
        SnakeController.OnMove -= OnMove;
        SnakeController.OnMoveBegin -= OnMoveBegin;
        SnakeController.OnMoveEnd -= OnMoveEnd;
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
