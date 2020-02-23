using UnityEngine;

public class Tags
{
    public const string SNAKE_HEAD = "Snake Head";
    public const string ENEMY = "Enemy";
    public const string SNAKE_BODY = "Snake Body";
}

public class CurrencyDespawner : MonoBehaviour
{
    private const float INITIAL_DESPAWN_TIME = 5.0f;

    private float startTime;

    private void OnEnable()
    {
        startTime = Time.time;
    }

    private void Update()
    {
        if (Time.time - startTime > INITIAL_DESPAWN_TIME)
        {
            Despawn();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if(collision.gameObject.CompareTag(Tags.SNAKE_HEAD))
        {
            Despawn();
        }
    }

    private void Despawn()
    {
        gameObject.SetActive(false);
    }
}
