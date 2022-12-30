using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class PlayerPickup : PlayerComponent
{
    private CircleCollider2D trigger;
    public float pickRadius
    {
        get
        {
            return trigger.radius;
        }
        set
        {
            trigger.radius = value;
        }
    }
    private void OnValidate()
    {
        trigger = GetComponent<CircleCollider2D>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        Pickable pickable;
        bool success = false;
        if (collision.gameObject.TryGetComponent<Pickable>(out pickable))
        {
            switch (pickable.type)
            {
                case ItemType.Ammo:
                    if (!player.HasWeapon())
                    {
                        return;
                    }
                    success = true;
                    break;
                case ItemType.Health:
                    if (player.HasFullHP())
                    {
                        return;
                    }

                    success = true;
                    break;
                case ItemType.Reserved1:
                    throw new NotImplementedException();
                case ItemType.Reserved2:
                    throw new NotImplementedException();
                default:
                    throw new NotImplementedException();
            }
        }
        if (success)
        {
            GameObject.Destroy(pickable.gameObject);
        }
    }

}
