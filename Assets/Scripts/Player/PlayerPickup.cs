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
        if (collision.gameObject.TryGetComponent<Pickable>(out pickable))
        {
            int toPick = 0;
            switch (pickable.type)
            {
                case ItemType.Ammo:
                    if (!player.HasWeapon())
                    {
                        break;
                    }
                    toPick = Mathf.Clamp(player.currentWeapon.gun.AmmoLimit - player.currentWeapon.currentAmmo, 0, pickable.amount);
                    player.currentWeapon.AddAmmo(toPick);
                    break;
                case ItemType.Health:
                    if (player.HasFullHP())
                    {
                        break;
                    }
                    break;
                case ItemType.Reserved1:
                    throw new NotImplementedException();
                case ItemType.Reserved2:
                    throw new NotImplementedException();
                default:
                    throw new NotImplementedException();
            }
            pickable.Pick(toPick);
        }
    }

}
