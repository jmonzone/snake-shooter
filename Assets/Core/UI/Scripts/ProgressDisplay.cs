using UnityEngine;
using UnityEngine.UI;

public class ProgressDisplay : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LevelManager progressManager;

    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        slider.maxValue = progressManager.Goal;
    }

    private void Start()
    {
        progressManager.OnProgressChanged += OnProgressChanged;
        progressManager.OnGoalChanged += OnGoalChanged;
    }

    private void OnGoalChanged(int goal)
    {
        slider.maxValue = goal;
    }

    private void OnProgressChanged(int progress)
    {
        slider.value = progress;
    }

}
