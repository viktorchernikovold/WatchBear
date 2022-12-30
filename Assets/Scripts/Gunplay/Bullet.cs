using UnityEngine;

public class Bullet : MonoBehaviour
{
    public BulletStats bullet;

    private void Update()
    {
        transform.Translate(transform.right * bullet.Speed * Time.deltaTime * Time.timeScale);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out LivingMixin living) && bullet.isFriendly != living.isFriendly)
        {
            living.Hurt(bullet.Damage);

            foreach (GameObject go in bullet.HitObjects)
                Instantiate(go, transform.position, transform.rotation);
        }
    }
}
