using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct TutorialPage
{
    [SerializeField] private Sprite sprite;
    [SerializeField] private string text;

    public Sprite Sprite => sprite;
    public string Text => text;
}

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private List<TutorialPage> tutorialPages;

    private int pageIndex = 0;

    public static event Action OnTutorialBegin;
    public static event Action OnTutorialEnd;
    public static event Action<TutorialPage> OnCurrentTutorialPageChanged;

    private void OnEnable()
    {
        Invoke(nameof(BeginTutorial), 1.0f);
        TutorialDisplay.OnContinueButtonPressed += OnContinueButtonPressed;
    }

    private void OnDisable()
    {
        TutorialDisplay.OnContinueButtonPressed -= OnContinueButtonPressed;
    }

    private void BeginTutorial()
    {
        if (PlayerPrefs.HasKey(PlayerPrefsKeys.TUTORIAL_KEY))
        {
            Debug.Log("Tutorial has already been done.");
            OnTutorialEnd?.Invoke();
        }
        else
        {
            PlayerPrefs.SetInt(PlayerPrefsKeys.TUTORIAL_KEY, 1);
            Debug.Log("Tutorial has begun.");
            OnTutorialBegin?.Invoke();
        }
    }

    private void OnContinueButtonPressed()
    {
        if (pageIndex < tutorialPages.Count)
        {
            UpdateCurrentTutorialPage();
        }
        else
        {
            Debug.Log("Tutorial has ended.");
            OnTutorialEnd?.Invoke();
        }
    }

    private void UpdateCurrentTutorialPage()
    {
        var currentPage = tutorialPages[pageIndex];
        OnCurrentTutorialPageChanged?.Invoke(currentPage);
        pageIndex++;
    }



}
