using UnityEngine;

public class LivingMixin : MonoBehaviour
{
    public delegate void OnHealthChange();
    public delegate void OnMaxHealthChange();
    public delegate void OnHeal();
    public delegate void OnHurt();
    public delegate void OnDeath();
    public event OnHealthChange onHealthChange;
    public event OnMaxHealthChange onMaxHealthChange;
    public event OnHeal onHeal;
    public event OnHurt onHurt;
    public event OnDeath onDeath;
    public bool isAlive;

    public float Hurt(float amount)
    {
        health -= amount;
        if (health > 0)
        {
            onHurt?.Invoke();
            onHealthChange?.Invoke();
            isAlive = true;
        }
        else
        {
            onDeath?.Invoke();
            onHealthChange?.Invoke();
            isAlive = false;
        }

        return health;
    }
    public float Heal(float amount)
    {
        health += amount;
        onHeal?.Invoke();
        onHealthChange?.Invoke();
        isAlive = health > 0 ? true : false;
        return health;
    }
    public float SetHealth(float newHealth)
    {
        float oldHealth = health;
        health = newHealth;

        if (health == 0)
        {
            onDeath?.Invoke();
            onHealthChange?.Invoke();
            isAlive = false;
        }
        else
        {
            isAlive = true;
            if (oldHealth > health)
            {
                onHurt?.Invoke();
                onHealthChange?.Invoke();
            }
            else
            {
                onHeal?.Invoke();
                onHealthChange?.Invoke();
            }
        }

        return health;
    }
    public void SetMaxHealth(float newHealth)
    {
        maxHealth = newHealth;
        onMaxHealthChange?.Invoke();
    }

    public float health
    {
        get => m_health;
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
