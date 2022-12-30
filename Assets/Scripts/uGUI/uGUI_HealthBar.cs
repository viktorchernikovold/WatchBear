using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uGUI_HealthBar : MonoBehaviour
{
    LivingMixin targetMixin;
    private void OnEnable()
    {
        targetMixin.onHealthChange += UpdateHealth;
    }
    private void OnDisable()
    {
        targetMixin.onHealthChange -= UpdateHealth;
    }
    private void UpdateHealth()
    {
        throw new NotImplementedException();
    }
}
