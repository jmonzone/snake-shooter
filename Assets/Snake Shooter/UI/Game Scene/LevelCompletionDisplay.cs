using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelCompletionDisplay : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject display;
    [SerializeField] private Text rewardsText;
    [SerializeField] private Text continueText;
    [SerializeField] private RewardedAdsButton rewardedAdsButton;

    private void OnEnable()
    {
        display.SetActive(false);

        LevelManager.OnLevelComplete += OnLevelComplete;
    }

    private void OnDisable()
    {
        LevelManager.OnLevelComplete -= OnLevelComplete;
    }

    private void OnLevelComplete(LevelCompleteEventArgs args)
    {
        display.SetActive(true);

        rewardedAdsButton.OnAdFinished += () =>
        {
            Debug.Log($"Ad watched. Player has earned {args.currencyReward} more scales.");
            GameManager.Instance.CurrencyCount += args.currencyReward;
            SceneManager.LoadScene(SceneNames.HOME_SCENE);
        };

        StartCoroutine(RewardTextUpdate(args.currencyReward));
        StartCoroutine(ContinueTextUpdate());
    }

    private IEnumerator RewardTextUpdate(int reward)
    {
        float i = 0;
        while (i <= reward)
        {
            i = Mathf.Clamp(i + 1, 0, reward);
            rewardsText.text = $"{(int)i}";
            yield return new WaitForFixedUpdate();
        }
    }

    private IEnumerator ContinueTextUpdate()
    {
        continueText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.0f);

        float a = 0;
        while (continueText.color.a < 255)
        {
            a = Mathf.Clamp(a + 1, 0, 255);
            continueText.color = new Color(continueText.color.a, continueText.color.b, continueText.color.b, a);
            yield return new WaitForFixedUpdate();
        }

    }
}
