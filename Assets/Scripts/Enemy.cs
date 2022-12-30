using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public float moveSpeed;
    public float playerMeetRange;
    public float playerFollowRange;
    public float attackRange;
    public float attackDamage;
    public float attackDelay;
    public float attackCooldown;

    public Entity target;

    protected virtual void Update()
    {
        if (target == null)
        {
            if (GameManager.main.objective == null || GameManager.main.objective.mixin.isAlive)
            {
                return;
            }
        }
        attackCooldown -= Time.deltaTime;
        if (attackCooldown < 0) attackCooldown = 0;

        if (InAttackRange())
        {
            if (!HasCooldown()) Attack();
        }
        else
        {
            Move();
        }
    }
    protected override void OnDie()
    {
        base.OnDie();
    }
    protected virtual void Move()
    {
        Vector2 direction = target.transform.position - transform.position;
        direction.Normalize();
        transform.Translate(direction * Time.deltaTime);
    }
    protected virtual void Attack()
    {
        attackCooldown = attackDelay;
        target.mixin.Hurt(attackDamage);
    }
    protected virtual bool InAttackRange()
    {
        if (target == null) return false;
        return Vector2.Distance(transform.position, target.transform.position) <= attackRange;
    }
    private bool HasCooldown()
    {
        return attackCooldown != 0;
    }
}
