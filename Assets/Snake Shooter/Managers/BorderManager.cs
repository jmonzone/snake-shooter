using System;
using UnityEngine;

public class BorderManager : MonoBehaviour
{
    public event Action OnSnakeHitBorder;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Snake Head"))
        {
            OnSnakeHitBorder?.Invoke();
        }
    }
}
