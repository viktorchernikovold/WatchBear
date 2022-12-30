using UnityEngine;

public class Bullet : MonoBehaviour
{
    public BulletStats bullet;
    public Rigidbody2D useRigidbody;
    public float timeInAir = 0.0f;
    public float dropTime;

    bool dropped = false;

    private void Start()
    {
        dropTime = Random.Range(bullet.DropTimeMin, bullet.DropTimeMax);
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
        useRigidbody.velocity = transform.up * bullet.Speed * speedMultiply;
    }

    public void Drop()
    {
        timeInAir = 0.0f;

        if (bullet.DroppedVariant != null)
            Instantiate(bullet.DroppedVariant, transform.position, transform.rotation);

        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Drop();

        if (collision.gameObject.TryGetComponent(out LivingMixin living) && bullet.isFriendly != living.isFriendly)
        {
            living.Hurt(bullet.Damage);
        }
    }
}
