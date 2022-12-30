using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerComponent : MonoBehaviour
{
    public Player player;
    private void OnValidate()
    {
        if (!TryGetComponent<Player>(out player))
        {
            Debug.LogError("A player component requires player!");
        }
    }
}
