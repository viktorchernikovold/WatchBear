using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public BulletStats bullet;
    public float timeInAir = 0.0f;
    public float dropTime;
    public bool dropped = false;

    private void Start()
    {
        dropTime = UnityEngine.Random.Range(bullet.DropTimeMin, bullet.DropTimeMax);
    }
    private void Update()
    {
        if (!dropped)
        {
            if (timeInAir < dropTime)
            {
                Fly();
            }
            else
            {
                Drop();
            }
        }
    }
    public void Fly()
    {
        float timeFracture = timeInAir / dropTime;
        float speedMultiply = bullet.DropSpeedCurve.Evaluate(timeFracture);
        timeInAir += Time.deltaTime;
        transform.Translate(Vector2.up * bullet.Speed * speedMultiply * Time.deltaTime * Time.timeScale);
    }

    public void Drop()
    {
        transform.rotation = Quaternion.Euler(0, 0, 90f);
        timeInAir = 0.0f;

        foreach (GameObject go in bullet.HitObjects)
            Instantiate(go, transform.position, transform.rotation);

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out LivingMixin living) && bullet.isFriendly != living.isFriendly)
        {
            living.Hurt(bullet.Damage);

            foreach (GameObject go in bullet.HitObjects)
                Instantiate(go, transform.position, transform.rotation);

            Destroy(gameObject);
        }
    }
}
