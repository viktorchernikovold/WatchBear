using UnityEngine;

[RequireComponent(typeof(LivingMixin))]
public class Entity : MonoBehaviour
{
    public LivingMixin mixin;

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

    public bool HasFullHP()
    {
        return mixin.health == mixin.maxHealth;
    }
    protected virtual void OnDie()
    {
        Destroy(this.gameObject);
    }
    protected virtual void OnHeal()
    {

    }
    protected virtual void OnHurt()
    {

    }
}
