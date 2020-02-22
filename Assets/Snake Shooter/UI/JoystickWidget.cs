﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickWidget : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform stick;
    [SerializeField] private Transform widgetBase;

    private Vector3 startPos;

    private void Awake()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;

            transform.position = mousePosition;
            stick.position = mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;

            var direction = mousePosition - widgetBase.position;
            if (direction.magnitude > .4f) direction = direction.normalized * 0.4f;

            stick.transform.position = widgetBase.position + direction;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            transform.position = startPos;
            stick.position = startPos;
        }
    }

}
