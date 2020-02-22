using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BackgroundManager : MonoBehaviour
{
    public event Action IsClicked;
    public event Action IsUnClicked;
    public event Action IsClicking;

    public void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            IsClicked?.Invoke();

        }
    }

    private void OnMouseDrag()
    {
        IsClicking?.Invoke();
    }

    public void OnMouseUp()
    {
        IsUnClicked?.Invoke();
    }
}
