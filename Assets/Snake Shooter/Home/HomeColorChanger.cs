using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeColorChanger : MonoBehaviour
{
    private HomeHealthManager homeHealthManager;
    private SpriteRenderer spriteRenderer;
    private Color DAMAGED_COLOR = Color.red;
    private Color HEALTHY_COLOR = Color.white;

    private void Start()
    {
        homeHealthManager = GetComponent<HomeHealthManager>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        homeHealthManager.OnEnemyHasReachedBase += ChangeColor;
    }


    private void ChangeColor()
    {
        StopAllCoroutines();
        StartCoroutine(ColorUpdate());
    }

    private IEnumerator ColorUpdate()
    {
        spriteRenderer.color = DAMAGED_COLOR;
        yield return new WaitForSeconds(1.0f);
        spriteRenderer.color = HEALTHY_COLOR;

    }
}
