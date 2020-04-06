using UnityEngine;

public class BackgroundColorChanger : MonoBehaviour
{
    private Renderer render;

    private void Awake()
    {
        render = GetComponent<Renderer>();
    }

    private void Start()
    {
        GameManager.Instance.OnCurrentLevelChanged += OnCurrentLevelChanged;
        ChangeColor(GameManager.Instance.CurrentLevel.BackgroundColor);
    }

    private void OnDisable()
    {
        GameManager.Instance.OnCurrentLevelChanged -= OnCurrentLevelChanged;
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
