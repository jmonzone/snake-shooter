using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoToStoreButton : MonoBehaviour
{
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(SceneNames.SHOP_SCENE);
        });
    }
}
