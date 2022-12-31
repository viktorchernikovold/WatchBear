using UnityEngine;

public class Entity : MonoBehaviour
{
    public Animator animator;
    public LivingMixin mixin;

    public GameObject deathObject;

    private void OnEnable()
    {
        mixin.onHeal += this.OnHeal;
        mixin.onHurt += this.OnHurt;
        mixin.onDeath += this.OnDie;
    }
    private void OnDisable()
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
        if (deathObject != null)
            Instantiate(deathObject, transform.position, transform.rotation);
        Destroy(gameObject);
    }
    protected virtual void OnHeal()
    {
        if (animator != null)
        {
            animator.SetTrigger("onHeal");
        }
    }
    protected virtual void OnHurt()
    {
        if (animator != null)
        {
            animator.SetTrigger("onHurt");
        }
    }
}
