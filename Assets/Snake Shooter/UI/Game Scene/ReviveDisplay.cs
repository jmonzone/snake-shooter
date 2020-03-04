using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReviveDisplay : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject reviveDisplay;
    [SerializeField] private RewardedAdsButton rewardedAdsButton;
    [SerializeField] private Button reviveButton;
    [SerializeField] private Text countdown;

    private const int COUNTDOWN_TIMER = 5;

    public static event Action OnReviveButtonClicked;

    private void OnEnable()
    {
        GameOverManager.OnGameOver += OnGameOver;
        rewardedAdsButton.OnAdFinished += OnAdFinished;

        reviveButton.onClick.AddListener(() =>
        {
            StopAllCoroutines();
            reviveDisplay.SetActive(false);
        });
    }

    private void OnDisable()
    {
        GameOverManager.OnGameOver -= OnGameOver;
        rewardedAdsButton.OnAdFinished -= OnAdFinished;
    }

    private void OnAdFinished()
    {
        Debug.Log("Ad finished.");
        StopAllCoroutines();
        OnReviveButtonClicked?.Invoke();
    }

    private void OnGameOver(GameOverEventArgs args)
    {
        if (args.CanRevive)
        {
            reviveDisplay.SetActive(true);
            StartCoroutine(CountdownUpdate());
        }
    }

    private IEnumerator CountdownUpdate()
    {
        for (int i = 0; i < 5; i++)
        {
            countdown.text = $"{COUNTDOWN_TIMER - i}";
            yield return new WaitForSeconds(1.0f);
        }

        Debug.Log("Countdown finished. Going back to the home screen.");
        SceneManager.LoadScene(SceneNames.HOME_SCENE);
    }
}
