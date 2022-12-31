using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject spawnee;
    public GameObject spawnEffect;

    public float radius;

    public float TimerMax;
    public float TimerMin;

    public int PlayerHPLowerThan;

    public bool MatchPlayerMissingHP;

    float timer;

    List<GameObject> gameObjects = new List<GameObject>();

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

            gameObjects.RemoveAll(item => item == null);

            if (MatchPlayerMissingHP && (Player.main.mixin.maxHealth - Player.main.mixin.health) < gameObjects.Count)
                return;

            Transform tr = Instantiate(spawnee, GetRandomPointWp(), Quaternion.identity).transform;
            gameObjects.Add(tr.gameObject);

            Instantiate(spawnEffect, tr.position, tr.rotation);

            timer = Random.Range(TimerMin, TimerMax);
        }
    }
}
