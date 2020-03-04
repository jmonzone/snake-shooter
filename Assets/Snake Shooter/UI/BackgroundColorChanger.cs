using UnityEngine;

public class BackgroundColorChanger : MonoBehaviour
{
    private Renderer render;

    private void Awake()
    {
        render = GetComponent<Renderer>();
    }

    private void OnEnable()
    {
        GameManager.OnCurrentLevelChanged += OnCurrentLevelChanged;
        ChangeColor(GameManager.Instance.CurrentLevel.BackgroundColor);
    }

    private void OnDisable()
    {
        GameManager.OnCurrentLevelChanged -= OnCurrentLevelChanged;
    }

    private void OnCurrentLevelChanged(ScriptableLevel level)
    {
        ChangeColor(level.BackgroundColor);
    }

    private void ChangeColor(Color color)
    {
        render.material.color = color;
    }
}
