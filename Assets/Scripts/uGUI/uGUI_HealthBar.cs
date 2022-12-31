using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uGUI_HealthBar : MonoBehaviour
{
    public RectTransform layoutGroup;
    public LivingMixin targetMixin;
    public List<Image> hearts = new List<Image>();
    public Sprite goodheart;
    public Sprite badheart;
    private void OnEnable()
    {
        UpdateHealth();
        targetMixin.onHealthChange += UpdateHealth;
    }
    private void OnDisable()
    {
        targetMixin.onHealthChange -= UpdateHealth;
    }
    private void UpdateHealth()
    {
        if (hearts.Count != targetMixin.maxHealth)
        {
            foreach (Image img in hearts)
            {
                Destroy(img.gameObject);
            }
            for (int i = 0; i < targetMixin.maxHealth; i++)
            {
                GameObject go = new GameObject("heart #" + i);
                go.transform.parent = this.transform;
                Image img = go.AddComponent<Image>();
                img.rectTransform.sizeDelta = new Vector2(layoutGroup.sizeDelta.y, layoutGroup.sizeDelta.y);
                hearts.Add(img);
            }
        }
        int g = targetMixin.health;
        foreach (Image heart in hearts)
        {
            if (g > 0)
            {
                heart.sprite = goodheart;
                g--;
            }
            else
            {
                heart.sprite = badheart;
            }
        }
    }
}
