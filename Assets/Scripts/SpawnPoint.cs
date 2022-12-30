using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public float radius;

    public Vector2 GetRandomPointWp()
    {
        Vector2 point = transform.position;
        Vector2 offset = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        offset.Normalize();
        offset *= radius * 2;

        return point * offset;
    }
}
