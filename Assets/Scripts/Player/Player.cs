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
        transform.position = new Vector3(0, 10000, 0);
        look.aimPoint.transform.position = new Vector3(0, 0, 0);
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
        if (!GameManager.main.hasLampBroke && GameManager.main.isPlaying)
        {
            GameManager.main.End(false);
        }
        Vector3 pos = transform.position;
        transform.position = new Vector3(0, 10000, 0);
        look.aimPoint.transform.position = pos;
    }
    public Vector2 GetPosition()
    {
        return transform.position;
    }
}
