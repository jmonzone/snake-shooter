using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickWidget : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform stick;
    [SerializeField] private Transform widgetBase;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gameObject.SetActive(true);

            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;

            widgetBase.position = mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;

            var direction = mousePosition - widgetBase.position;
            if (direction.magnitude > .75f) direction.Normalize();

            stick.transform.position = widgetBase.position + direction;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            gameObject.SetActive(false);

        }
    }

}
