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
    public Rigidbody2D useRigidbody;

    protected virtual void Update()
    {
        bool flag1 = GameManager.main.objective != null || GameManager.main.objective.mixin.isAlive; // is Objective Alive
        float num1 = Player.main != null ? Vector2.Distance(Player.main.GetPosition(), transform.position) : 1000f;
        float num2 = flag1 ? Vector2.Distance(GameManager.main.objective.transform.position, transform.position) : 1000f;

        if (target == null || !target.mixin.isAlive)
        {
            if (flag1)
            {
                target = GameManager.main.objective;
            }
        }
        if (num1 <= playerMeetRange && num2 > num1)
        {
            target = Player.main;
        }
        if ((num1 > playerFollowRange || num2 <= num1) && flag1)
        {
            target = GameManager.main.objective;
        }

        attackCooldown -= Time.deltaTime;
        if (attackCooldown < 0) attackCooldown = 0;

        if (InAttackRange())
        {
            if (!HasCooldown()) Attack();
        }
        else
        {
            if (target != null)
            {
                Move();
            }
            return;
        }
    }
    protected override void OnDie()
    {
        base.OnDie();
    }
    protected virtual void LateUpdate()
    {
        animator.SetBool("isMoving", useRigidbody.velocity.magnitude > 0);
    }
    protected virtual void Move()
    {
        Vector2 direction = target.transform.position - transform.position;
        direction.Normalize();
        direction *= moveSpeed;
        useRigidbody.velocity = direction;
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
