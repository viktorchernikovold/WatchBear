using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class PlayerPickup : PlayerComponent
{
    List<Pickable> pickables = new List<Pickable>();

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

    private void Update()
    {
        if (pickables.Count > 0)
        {
            for (int i = 0; i < pickables.Count; i++)
            {
                int toPick = 0;
                switch (pickables[i].type)
                {
                    case ItemType.Ammo:
                        if (!player.HasWeapon())
                        {
                            break;
                        }
                        toPick = Mathf.Clamp(player.currentWeapon.gun.AmmoLimit - player.currentWeapon.currentAmmo, 0, pickables[i].amount);
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
                pickables[i].Pick(toPick);
            }

            pickables.RemoveAll(item => item == null);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Pickable pickable;
        if (collision.gameObject.TryGetComponent(out pickable))
        {
            pickables.Add(pickable);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Pickable pickable;
        if (collision.gameObject.TryGetComponent(out pickable) && !pickables.Contains(pickable))
        {
            Debug.Log("TriggerStay");
            pickables.Add(pickable);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Pickable pickable;
        if (collision.gameObject.TryGetComponent(out pickable))
        {
            pickables.Remove(pickable);
        }
    }

}
