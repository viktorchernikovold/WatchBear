using UnityEngine;

public class Boss : MonoBehaviour
{
    private void OnEnable()
    {
        BossBar.main.Boss = GetComponent<LivingMixin>();
    }

    private void OnDisable()
    {
        BossBar.main.Boss = null;
    }
}
