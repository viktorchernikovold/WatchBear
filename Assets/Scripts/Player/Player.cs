using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Gun currentWeapon;
    public PlayerLook look;
    public PlayerMovement movement;
    public LivingMixin healthSystem;

    public static Player main;

    private void OnEnable()
    {
        healthSystem.onDeath += OnDie;
    }
    private void OnDisable()
    {
        healthSystem.onDeath -= OnDie;
    }
    private void Awake()
    {
        main = this;
    }
    private void OnValidate()
    {
        look = GetComponent<PlayerLook>();
        movement = GetComponent<PlayerMovement>();
        healthSystem = GetComponent<LivingMixin>();
    }
    public bool HasFullHP()
    {
        return healthSystem.health == healthSystem.maxHealth;
    }
    public bool HasWeapon()
    {
        return currentWeapon != null;
    }
    private void OnDie()
    {
        throw new NotImplementedException();
    }
    public Vector3 GetPosition()
    {
        return transform.position;
    }
}
