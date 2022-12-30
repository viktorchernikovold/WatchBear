using UnityEngine;

[CreateAssetMenu(fileName = "Bullet", menuName = "Bullet")]
public class BulletStats : ScriptableObject
{
    public float Damage = 10f;
    public float Speed = 7f;

    public GameObject[] HitObjects;

    public bool isFriendly;
}
