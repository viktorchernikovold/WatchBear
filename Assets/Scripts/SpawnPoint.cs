using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject spawnee;
    public GameObject spawnEffect;

    public float radius;

    public float TimerMax;
    public float TimerMin;

    public int PlayerHPLowerThan;

    float timer;

    public Vector2 GetRandomPointWp()
    {
        Vector2 point = transform.position;
        Vector2 offset = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        offset *= radius * 2;

        return point + offset;
    }

    private void Awake()
    {
        timer = Random.Range(TimerMin, TimerMax);
    }
    public void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(transform.position, radius * 2);
    }

    private void Update()
    {
        if (timer > 0f)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            if (Player.main.mixin.health >= PlayerHPLowerThan)
                return;

            Transform tr = Instantiate(spawnee, GetRandomPointWp(), Quaternion.identity).transform;

            Instantiate(spawnEffect, tr.position, tr.rotation);

            timer = Random.Range(TimerMin, TimerMax);
        }
    }
}
