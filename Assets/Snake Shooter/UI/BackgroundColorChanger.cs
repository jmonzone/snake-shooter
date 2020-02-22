using System;
using UnityEngine;

public class BackgroundColorChanger : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LevelManager levelManager;

    [Header("Options")]
    [SerializeField] private Color[] colors;

    private void Start()
    {
        LevelManager.OnLevelChanged += OnLevelChanged;
        Camera.main.backgroundColor = colors[0];
    }

    private void OnLevelChanged(int level)
    {
        for (int i = 0; i < colors.Length; i++)
        {
            if (level <= (i + 1) * 5)
            {
                Camera.main.backgroundColor = Color.Lerp(colors[i], colors[i + 1], (level % 5) / 5.0f);
                return;
            }
        }
    }
}
