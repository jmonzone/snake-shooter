using System;
using System.Collections;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    [Header("Options")]
    [SerializeField] private float sensitivity = 0.75f;
    [SerializeField] private float maxSpeed = 4.0f;

    private float MAX_HEIGHT => Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y + 0.25f;
    private float MIN_HEIGHT => Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y - 1.25f;
    private float MAX_WIDTH => Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x + 0.25f;
    private float MIN_WIDTH => Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x - 0.25f;

    public static event Action<Vector3> OnMove;
    public static event Action OnMoveBegin;
    public static event Action OnMoveEnd;

    private void OnEnable()
    {
        LevelManager.OnRoundBegun += OnRoundBegun;
        LevelManager.OnRoundEnded += OnRoundEnded;
        GameOverManager.OnGameOver += OnGameOver;
    }

    private void OnDisable()
    {
        LevelManager.OnRoundBegun -= OnRoundBegun;
        LevelManager.OnRoundEnded -= OnRoundEnded;
        GameOverManager.OnGameOver -= OnGameOver;
    }

    private void OnRoundBegun(OnRoundBegunEventArgs args)
    {
        StopAllCoroutines();

        var pref = PlayerPrefs.GetInt(PlayerPrefsKeys.SNAKE_MOVEMENT, 0);
        var update = pref == 0 ? FollowUpdate() : JoystickUpdate();

        StartCoroutine(update);
    }

    private void OnRoundEnded(int round)
    {
        StopAllCoroutines();
    }

    private void OnGameOver(GameOverEventArgs args)
    {
        StopAllCoroutines();
    }

    private IEnumerator FollowUpdate()
    {
        yield return new WaitForSeconds(0.5f);

        while (true)
        {
            yield return new WaitUntil(() => Input.GetMouseButton(0));
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;

            var direction = mousePos - transform.position;
            direction = Vector3.ClampMagnitude(direction * sensitivity, maxSpeed);

            transform.up = direction;

            transform.position += direction * Time.deltaTime;
        }
    }

    private IEnumerator JoystickUpdate()
    {
        while (enabled)
        {
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

            OnMoveBegin?.Invoke();

            var startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            startPos.z = 0;

            while (Input.GetMouseButton(0))
            {
                var endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                endPos.z = 0;

                var direction = endPos - startPos;
                direction = Vector3.ClampMagnitude(direction * sensitivity, maxSpeed);

                var temp = transform.position + (direction * Time.deltaTime);

                if (temp.x < MAX_WIDTH || temp.x > MIN_WIDTH)
                {
                    direction.x = 0;
                }
                if (temp.y < MAX_HEIGHT || temp.y > MIN_HEIGHT)
                {
                    direction.y = 0;
                }

                transform.up = direction;

                transform.position += direction * Time.deltaTime;

                OnMove?.Invoke(transform.position + direction);

                yield return new WaitForFixedUpdate();
            }

            OnMoveEnd?.Invoke();
        }
    }
}
