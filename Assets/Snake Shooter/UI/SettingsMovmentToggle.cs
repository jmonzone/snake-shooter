using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMovmentToggle : MonoBehaviour
{
    private void Awake()
    {
        var toggle = GetComponent<Toggle>();
        toggle.isOn =  PlayerPrefs.GetInt(PlayerPrefsKeys.SNAKE_MOVEMENT, 0) == 1;

        toggle.onValueChanged.AddListener((value) =>
        {
            PlayerPrefs.SetInt(PlayerPrefsKeys.SNAKE_MOVEMENT, value ? 1 : 0);
        });
    }
}
