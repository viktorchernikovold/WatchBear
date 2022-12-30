using UnityEngine;

public class LivingMixin : MonoBehaviour
{
    public delegate void OnHeal();
    public delegate void OnHurt();
    public delegate void OnDeath();
    public event OnHeal onHeal;
    public event OnHurt onHurt;
    public event OnDeath onDeath;

    public float Hurt(float amount)
    {
        health -= amount;
        if (health > 0)
        {
            onHurt?.Invoke();
        }
        else
        {
            onDeath?.Invoke();
        }

        return health;
    }
    public float Heal(float amount)
    {
        health += amount;
        onHeal?.Invoke();

        return health;
    }
    public float SetHealth(float newHealth)
    {
        float oldHealth = health;
        health = newHealth;

        if (health == 0)
        {
            onDeath?.Invoke();
        }
        else
        {
            if (oldHealth > health)
            {
                onHurt?.Invoke();
            }
            else
            {
                onHeal?.Invoke();
            }
        }

        return health;
    }
    public void SetMaxHealth(float newHealth)
    {
        maxHealth = newHealth;
    }

    public float health
    {
        get => health;
        private set => m_health = Mathf.Clamp(value, 0f, maxHealth);
    }
    public float maxHealth
    {
        get => m_maxHealth;
        private set => m_maxHealth = Mathf.Clamp(value, 1f, Mathf.Infinity);
    }
    [SerializeField]
    private float m_health;
    [SerializeField]
    private float m_maxHealth;

    public bool isFriendly;
}
