using UnityEngine;

[CreateAssetMenu(fileName = "Bullet", menuName = "Bullet")]
public class BulletStats : ScriptableObject
{
    public int Damage = 1;
    public float Speed = 7f;

    public float DropTimeMin;
    public float DropTimeMax;
    public float ricochetSpeedDrop = 0.6f;
    public AnimationCurve DropSpeedCurve;

    public GameObject DroppedVariant;
    public GameObject[] HitObjects;

    public bool isFriendly;
}
