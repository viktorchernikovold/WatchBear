using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed;
    public float playerMeetRange;
    public float playerFollowRange;
    public float attackRange;
    public float attackDamage;
    public float attackDelay;
    public float attackCooldown;

    LivingMixin target;


    private void Update()
    {
        attackCooldown -= Time.deltaTime;
        if (attackCooldown < 0) attackCooldown = 0;


        if (InAttackRange() && !HasCooldown())
        {
            Attack();
        }
        else
        {
            Move();
        }
    }
    private void Move()
    {
        Vector2 direction = transform.position - target.transform.position;
        direction.Normalize();
        transform.Translate(direction * Time.deltaTime);
    }
    private void Attack()
    {
        attackCooldown = attackDelay;
        target.Hurt(attackDamage);
    }
    private bool InAttackRange()
    {
        if (target == null) return false;
        return Vector2.Distance(transform.position, target.transform.position) <= attackRange;
    }
    private bool HasCooldown()
    {
        return attackCooldown != 0;
    }
}
