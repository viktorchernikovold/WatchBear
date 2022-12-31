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

    private void OnEnable()
    {
        isAlive = health > 0;
    }
    public int Hurt(int amount)
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
    public int Heal(int amount)
    {
        health = Mathf.Clamp(health + amount, 0, m_maxHealth);
        onHeal?.Invoke();
        onHealthChange?.Invoke();
        isAlive = health > 0 ? true : false;
        return health;
    }
    public int SetHealth(int newHealth)
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
    public void SetMaxHealth(int newHealth)
    {
        maxHealth = newHealth;
        onMaxHealthChange?.Invoke();
    }

    public int health
    {
        get => m_health;
        private set => m_health = Mathf.Clamp(value, 0, maxHealth);
    }
    public int maxHealth
    {
        get => m_maxHealth;
        private set => m_maxHealth = Mathf.Clamp(value, 1, 1000);
    }
    [SerializeField]
    private int m_health;
    [SerializeField]
    private int m_maxHealth;

    public bool isFriendly;
}
