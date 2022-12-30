using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    public int amount;
    public ItemType type;

    public void Pick(int amount)
    {
        this.amount -= amount;
        if (this.amount <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
