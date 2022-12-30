using UnityEngine;

public class Gun : MonoBehaviour
{
    public GunStats gun;

    [HideInInspector]
    public int currentAmmo;

    [SerializeField]
    ParticleSystem par;

    float delay;

    private void Awake()
    {
        currentAmmo = gun.AmmoLimit;
        delay = 0f;
    }

    private void Update()
    {
        if (delay > 0f) delay -= Time.deltaTime * Time.timeScale;

        if (Input.GetMouseButton(0))
        {
            Shoot();
        }
    }
    public void AddAmmo(int amount)
    {
        currentAmmo += amount;
        if (currentAmmo > gun.AmmoLimit) currentAmmo = gun.AmmoLimit;
    }
    public void Shoot()
    {
        if (currentAmmo <= 0 || delay > 0f)
            return;

        currentAmmo -= 1;
        delay = 1f / gun.RPS;
        Instantiate(gun.BulletToSpawn, transform.position, transform.rotation);

        if (par != null)
            par.Play();
    }
}
