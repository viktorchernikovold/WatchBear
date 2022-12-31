using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uGUI_AmmoBar : MonoBehaviour
{
    public RectTransform layoutGroup;
    public Gun targetWeapon;
    public List<Image> ammos = new List<Image>();
    public Sprite goodammo;
    public Sprite badammo;
    private void OnEnable()
    {
        UpdateHealth();
        targetWeapon.onAmmoChange += UpdateHealth;
    }
    private void OnDisable()
    {
        targetWeapon.onAmmoChange -= UpdateHealth;
    }
    private void UpdateHealth()
    {
        if (ammos.Count != targetWeapon.gun.AmmoLimit)
        {
            foreach (Image img in ammos)
            {
                Destroy(img.gameObject);
            }
            for (int i = 0; i < targetWeapon.gun.AmmoLimit; i++)
            {
                GameObject go = new GameObject("ammo #" + i);
                go.transform.parent = this.transform;
                Image img = go.AddComponent<Image>();
                img.rectTransform.sizeDelta = new Vector2(layoutGroup.sizeDelta.y, layoutGroup.sizeDelta.y);
                ammos.Add(img);
            }
        }
        int g = targetWeapon.currentAmmo;
        foreach (Image heart in ammos)
        {
            if (g > 0)
            {
                heart.sprite = goodammo;
                g--;
            }
            else
            {
                heart.sprite = badammo;
            }
        }
    }
}
