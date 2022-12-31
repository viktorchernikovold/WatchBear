using UnityEngine;

public class Bullet : MonoBehaviour
{
    public BulletStats bullet;
    public Rigidbody2D useRigidbody;
    public float timeInAir = 0.0f;
    public float dropTime;
    public float ricochetSpeedDrop;

    bool dropped = false;

    private void Start()
    {
        dropTime = Random.Range(bullet.DropTimeMin, bullet.DropTimeMax);
        ricochetSpeedDrop = 1.0f;
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
        useRigidbody.velocity = transform.up * bullet.Speed * speedMultiply * ricochetSpeedDrop;
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
        Vector2 direction = Vector2.Reflect(transform.up, collision.contacts[0].normal);
        float angle = Mathf.Atan2(-direction.x, direction.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        ricochetSpeedDrop *= bullet.ricochetSpeedDrop;

        if (bullet.HitObjects.Length > 0)
        {
            foreach (GameObject go in bullet.HitObjects)
            {
                Instantiate(go, transform.position, transform.rotation);
            }
        }

        if (collision.gameObject.TryGetComponent(out LivingMixin living))
        {
            if (living.isFriendly != bullet.isFriendly)
            {
                living.Hurt(bullet.Damage);
            }
        }
    }
}
