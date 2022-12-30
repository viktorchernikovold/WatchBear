using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    public float pickRadius = 2f;
    private void Update()
    {
        
    }
    private Pickable[] GetPickablesInRadius(float radius)
    {
        return new Pickable[4];
    }
}
