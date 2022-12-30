using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject spawnee;

    public float radius;

    public float TimerMax;
    public float TimerMin;

    float timer;

    public Vector2 GetRandomPointWp()
    {
        Vector2 point = transform.position;
        Vector2 offset = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        offset.Normalize();
        offset *= radius * 2;

        return point * offset;
    }

    private void Awake()
    {
        timer = Random.Range(TimerMin, TimerMax);
    }

    private void Update()
    {
        if (timer > 0f)
            timer -= Time.deltaTime;
        else
        {
            Instantiate(spawnee, GetRandomPointWp(), Quaternion.identity);
            timer = Random.Range(TimerMin, TimerMax);
        }
    }
}
