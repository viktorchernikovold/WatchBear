using Game.Utils;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public Animator animator;
    public LivingMixin mixin;

    public GameObject deathObject;
    public GameObject hurtObject;

    public AudioClip deathSound;
    public AudioClip hurtSound;
    public AudioClip healSound;

    protected virtual void OnEnable()
    {
        mixin.onHeal += this.OnHeal;
        mixin.onHurt += this.OnHurt;
        mixin.onDeath += this.OnDie;
    }
    protected virtual void OnDisable()
    {
        mixin.onHeal -= this.OnHeal;
        mixin.onHurt -= this.OnHurt;
        mixin.onDeath -= this.OnDie;
    }

    private void OnValidate()
    {
        if (animator == null)
        {
            if (!TryGetComponent<Animator>(out animator))
            {
                Debug.LogWarning($"Can't find Animator component for {gameObject.name}.");
            }
        }
        if (mixin == null)
        {
            if (!TryGetComponent<LivingMixin>(out mixin))
            {
                Debug.LogError($"LivingMixin component is reqired for {gameObject.name}!");
            }
        }
    }

    public bool HasFullHP()
    {
        return mixin.health == mixin.maxHealth;
    }
    protected virtual void OnDie()
    {
        if (deathSound != null)
            AudioSystem.PlaySound(deathSound, transform.position, 1f, 128);
        if (deathObject != null)
            Instantiate(deathObject, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    protected virtual void OnHeal()
    {
        if (healSound != null)
            AudioSystem.PlaySound(healSound, transform.position, 1f, 128);
    }

    protected virtual void OnHurt()
    {
        if (hurtSound != null)
            AudioSystem.PlaySound(hurtSound, transform.position, 1f, 128);

        if (hurtObject != null)
            Instantiate(hurtObject, transform.position, transform.rotation);
    }
}
