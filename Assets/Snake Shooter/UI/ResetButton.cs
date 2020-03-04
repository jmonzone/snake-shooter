using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetButton : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Button resetButton;
    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;

    [SerializeField] private GameObject warningDisplay;

    private void Awake()
    {
        resetButton.onClick.AddListener(() => DisplayWarning());
        yesButton.onClick.AddListener(() =>
        {
            ResetProgress();
            DisplayWarning(false);
        });
        noButton.onClick.AddListener(() => DisplayWarning(false));
    }

    private void DisplayWarning(bool show = true)
    {
        warningDisplay.SetActive(show);
    }

    private void ResetProgress()
    {
        PlayerPrefs.DeleteAll();

        GameManager.Instance.ResetAvailableTowers();
    }
}
