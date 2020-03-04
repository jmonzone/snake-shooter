using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectionDisplay : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Text stageNameText;
    [SerializeField] private Image stageImage;
    [SerializeField] private Button stageImageButton;
    [SerializeField] private Button playButton;

    [SerializeField] private Button nextStageButton;
    [SerializeField] private Button previousStageButton;

    private void Start()
    {
        playButton.onClick.AddListener(() => EnterLevel());
        stageImageButton.onClick.AddListener(() => EnterLevel());

        nextStageButton.onClick.AddListener(() => SwitchLevel(true));
        previousStageButton.onClick.AddListener(() => SwitchLevel(false));

        var levelIndex = GameManager.Instance.HighestLevel - 1;
        var level = GameManager.Instance.CurrentLevel = GameManager.Instance.AvailableLevels[levelIndex];
        UpdateLevelSelectionDisplay(level);
    }

    private void SwitchLevel(bool next = true)
    {
        var levels = GameManager.Instance.AvailableLevels;
        var currentLevel = GameManager.Instance.CurrentLevel;
        var levelIndex = currentLevel.ID - 1;

        var value = next ? levelIndex + 1 : levelIndex - 1;
        levelIndex = Mathf.Clamp(value, 0, levels.Count - 1);

        Debug.Log($"Switching to level {levelIndex + 1}.");

        var level = levels[levelIndex];
        GameManager.Instance.CurrentLevel = level;
        UpdateLevelSelectionDisplay(level);
    }

    private void EnterLevel()
    {
        SceneManager.LoadScene(SceneNames.GAME_SCENE);
    }

    private void UpdateLevelSelectionDisplay(ScriptableLevel level)
    {
        var playable = GameManager.Instance.HighestLevel >= level.ID;

        playButton.interactable = stageImageButton.interactable = playable;

        stageImage.sprite = level.Sprite;

        stageNameText.text = playable ? $"World {level.ID}" : $"Complete World {level.ID - 1} to unlock";

        previousStageButton.gameObject.SetActive(level.ID != 1);
        nextStageButton.gameObject.SetActive(level.ID != GameManager.Instance.AvailableLevels.Count);
    }
}
