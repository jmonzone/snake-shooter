using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Speed speed;
    public Speed Speed => speed;

    protected Health health;
    public Health Health => health;

    protected Status status;
    public Status Status => status; 

    private void Awake()
    {
        speed = GetComponent<Speed>();
        health = GetComponent<Health>();
        status = GetComponent<Status>();
    }

    private void OnEnable()
    {
        float rand = Random.Range(1, 10);
        float spawnx = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width * (rand / 10.0f), 0)).x;
        float spawnY = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y;
        transform.position = new Vector2(spawnx, spawnY);
    }

    private void Update()
    {
        transform.position += Vector3.down * Time.deltaTime * speed.Value;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Base"))
        {
            Health.Value = 0;
        }
    }

}
