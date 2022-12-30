using UnityEngine;

[CreateAssetMenu(fileName = "Bullet", menuName = "Bullet")]
public class BulletStats : ScriptableObject
{
    public float Damage = 10f;
    public float Speed = 7f;

    public float DropTimeMin;
    public float DropTimeMax;
    public AnimationCurve DropSpeedCurve;

    public GameObject DroppedVariant;
    public GameObject[] HitObjects;

    public bool isFriendly;
}
