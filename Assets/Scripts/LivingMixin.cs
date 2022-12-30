using UnityEngine;

public class LivingMixin : MonoBehaviour
{
    public delegate void OnDeath();
    public delegate void OnHurt();
    public event OnDeath onDeath;
    public event OnHurt onHurt;
    public void Hurt(float amount)
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
    }
    public void Heal(float amount)
    {

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
}
