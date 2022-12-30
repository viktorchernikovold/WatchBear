using UnityEngine;

public class Pickable : MonoBehaviour
{
    public int amount;
    public float cooldownTime = 1f;
    public ItemType type;

    [SerializeField]
    float timer;

    private void Awake()
    {
        timer = cooldownTime;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
    }

    public void Pick(int _amount)
    {
        if (timer > 0f)
            return;

        amount -= _amount;

        if (amount <= 0)
        {
            Destroy(gameObject);
        }
    }
}
