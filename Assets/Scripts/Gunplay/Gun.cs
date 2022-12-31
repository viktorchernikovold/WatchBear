using Game.Utils;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GunStats gun;

    public delegate void OnAmmoChange();
    public event OnAmmoChange onAmmoChange;

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
        if (delay > 0f) delay -= Time.deltaTime;

        if (Input.GetMouseButton(0) && GameManager.main.isPlaying)
        {
            Shoot();
        }
    }
    public void AddAmmo(int amount)
    {
        currentAmmo += amount;
        if (currentAmmo > gun.AmmoLimit) currentAmmo = gun.AmmoLimit;
        onAmmoChange?.Invoke();
    }
    public void Shoot()
    {
        if (currentAmmo <= 0 || delay > 0f)
            return;

        currentAmmo -= 1;
        onAmmoChange?.Invoke();

        AudioSystem.PlaySound(gun.ShootSound, transform.position, 1f, 128);
        
        delay = 1f / gun.RPS;
        Instantiate(gun.BulletToSpawn, transform.position, transform.rotation);

        if (par != null)
            par.Play();
    }
}
