using UnityEngine;
using UnityEngine.UI;

public class ProgressDisplay : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LevelManager levelManager;

    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        slider.maxValue = levelManager.Goal;
    }

    private void Start()
    {
        LevelManager.OnGoalChanged += OnGoalChanged;
    }

    private void OnGoalChanged(int goal)
    {
        slider.maxValue = goal;
        slider.value = levelManager.Progress;
    }

    private void Update()
    {
        var speed = levelManager.Progress - slider.value > 1 ? 1 : 10;
        slider.value += (levelManager.Progress - slider.value) * Time.deltaTime * speed;
    }

}
