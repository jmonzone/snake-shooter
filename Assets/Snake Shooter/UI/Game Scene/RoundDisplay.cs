using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RoundDisplay : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject display;
    [SerializeField] private Text text;

    private void OnEnable()
    {
        LevelManager.OnRoundBegun += UpdateRoundDisplay;
    }

    private void OnDisable()
    {
        LevelManager.OnRoundBegun -= UpdateRoundDisplay;
    }

    private void UpdateRoundDisplay(OnRoundBegunEventArgs args)
    {
        display.SetActive(true);

        text.text = $"Round {args.currentRound} of {args.roundCount}";

        StopAllCoroutines();
        StartCoroutine(DisableUpdate());
    }

    private IEnumerator DisableUpdate()
    {
        yield return new WaitForSeconds(3.0f);
        display.SetActive(false);
    }
}
