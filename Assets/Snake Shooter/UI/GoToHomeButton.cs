using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoToHomeButton : MonoBehaviour
{
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(SceneNames.HOME_SCENE);
        });
    }
}
