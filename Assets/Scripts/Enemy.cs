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
    public float attackRate;
    public float attackDamage;
    public float attackDelay;
    public float attackCooldown;

    LivingMixin target;


    private void Update()
    {
        attackCooldown -= Time.deltaTime;
        if (attackCooldown < 0) attackCooldown = 0;
    }
    private void Move()
    {
        throw new NotImplementedException();
    }
    private void Attack()
    {
        attackCooldown = attackDelay;
        target.Hurt(attackDamage);
    }
}
