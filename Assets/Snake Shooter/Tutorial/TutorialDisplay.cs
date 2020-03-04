using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialDisplay : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject display;
    [SerializeField] private Text text;
    [SerializeField] private Image image;
    [SerializeField] private Button button;

    public static event Action OnContinueButtonPressed;

    private void OnEnable()
    {
        button.onClick.AddListener(() => OnContinueButtonPressed?.Invoke());

        TutorialManager.OnTutorialBegin += OnTutorialBegin;
        TutorialManager.OnTutorialEnd += OnTutorialEnd;
        TutorialManager.OnCurrentTutorialPageChanged += OnCurrentTutorialPageChanged;
    }

    private void OnDisable()
    {
        TutorialManager.OnTutorialBegin -= OnTutorialBegin;
        TutorialManager.OnTutorialEnd -= OnTutorialEnd;
        TutorialManager.OnCurrentTutorialPageChanged -= OnCurrentTutorialPageChanged;
    }

    private void OnCurrentTutorialPageChanged(TutorialPage tutorialPage)
    {
        display.SetActive(true);
        text.text = tutorialPage.Text;
        image.sprite = tutorialPage.Sprite;
    }

    private void OnTutorialBegin()
    {
        display.SetActive(true);
    }

    private void OnTutorialEnd()
    {
        display.SetActive(false);
    }
}
