using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneNames
{
    public const string HOME_SCENE = "Home Scene";
    public const string GAME_SCENE = "Game Scene";
    public const string SHOP_SCENE = "Shop Scene";
    public const string SETTINGS_SCENE = "Settings Scene";

}

public class GameOverDisplay : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject display;
    [SerializeField] private RewardedAdsButton rewardedAdsButton;
    [SerializeField] private Button reviveButton;
    [SerializeField] private Text countdown;

    private const int COUNTDOWN_TIMER = 5;

    public event Action OnReviveButtonClicked;

    private void Start()
    {
        GameOverManager.OnGameOver += OnGameOver;

        rewardedAdsButton.OnAdFinished += () =>
        {
            OnReviveButtonClicked?.Invoke();
        };

        reviveButton.onClick.AddListener(() =>
        {
            StopAllCoroutines();
            display.SetActive(false);
        });
    }

    private void OnGameOver()
    {
        display.SetActive(true);
        StartCoroutine(CountdownUpdate());
    }

    private IEnumerator CountdownUpdate()
    {
        for (int i = 0; i < 5; i++)
        {
            countdown.text = $"{COUNTDOWN_TIMER - i}";
            yield return new WaitForSeconds(1.0f);
        }

        SceneManager.LoadScene(SceneNames.HOME_SCENE);
    }
}
