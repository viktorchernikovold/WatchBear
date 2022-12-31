using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "Gun")]
public class GunStats : ScriptableObject
{
    public float RPS = 2;

    public int AmmoLimit = 5;

    public GameObject BulletToSpawn;

    public AudioClip ShootSound;
}
