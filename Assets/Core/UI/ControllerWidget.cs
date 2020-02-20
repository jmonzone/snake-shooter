using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerWidget : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private BackgroundManager backgroundManager;

    [SerializeField] private Transform stick;
    [SerializeField] private Transform widgetBase;


    private void Start()
    {
        backgroundManager.IsClicking += OnMove;
        backgroundManager.IsClicked += OnMoveBegin;
        backgroundManager.IsUnClicked += OnMoveEnd;

        gameObject.SetActive(false);
    }

    private void OnMove()
    {
        var newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0;

        var direction = newPosition - widgetBase.position;
        if (direction.magnitude > .75f) direction.Normalize();

        stick.transform.position = widgetBase.position + direction;
    }

    private void OnMoveBegin()
    {
        gameObject.SetActive(true);

        var newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0;

        widgetBase.position = newPosition;
    }

    private void OnMoveEnd()
    {
        gameObject.SetActive(false);
    }

}
