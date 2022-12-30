using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public BulletStats bullet;
    public float timeInAir = 0.0f;
    public bool dropped = false;

    private void Update()
    {
        if (!dropped)
        {
            if (timeInAir < bullet.DropTime)
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
        float timeFracture = timeInAir / bullet.DropTime;
        float speedMultiply = bullet.DropSpeedCurve.Evaluate(timeFracture);
        timeInAir += Time.deltaTime;
        transform.Translate(Vector2.up * bullet.Speed * speedMultiply * Time.deltaTime * Time.timeScale);
    }

    public void Drop()
    {
        transform.rotation = Quaternion.Euler(Vector3.right);
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
