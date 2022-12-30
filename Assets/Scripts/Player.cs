using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerLook look;
    public PlayerMovement movement;
    public LivingMixin healthSystem;

    public static Player main;

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
    public Vector3 GetPosition()
    {
        return transform.position;
    }
}
