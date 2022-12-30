using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public Gun currentWeapon;
    public PlayerLook look;
    public PlayerMovement movement;

    public static Player main;

    private void Awake()
    {
        if (main == null) main = this;
        else Destroy(gameObject);
    }
    private void OnValidate()
    {
        look = GetComponent<PlayerLook>();
        movement = GetComponent<PlayerMovement>();
        mixin = GetComponent<LivingMixin>();
    }
    public bool HasWeapon()
    {
        return currentWeapon != null;
    }

    protected override void OnDie()
    {
        base.OnDie();
    }
    public Vector3 GetPosition()
    {
        return transform.position;
    }
}
