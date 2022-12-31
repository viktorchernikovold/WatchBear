using UnityEngine;
using UnityEngine.UI;

public class BossBar : MonoBehaviour
{
    public static BossBar main;

    [HideInInspector]
    public LivingMixin Boss;

    [SerializeField]
    Slider bossbar;

    private void Awake()
    {
        if (main == null) main = this;
        else Destroy(gameObject);
    }

    private void Update()
    {
        if (Boss != null)
        {
            bossbar.gameObject.SetActive(true);
            bossbar.value = Boss.health;
        }
        else
            bossbar.gameObject.SetActive(false);
    }
}
