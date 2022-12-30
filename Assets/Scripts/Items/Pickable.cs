using UnityEngine;

public class Pickable : MonoBehaviour
{
    public int amount;
    public float pickCooldown = 1f;
    public ItemType type;
    public Collider2D useCollider;

    private void OnValidate()
    {
        if (!TryGetComponent<Collider2D>(out useCollider))
        {
            Debug.LogError($"PICKABLE \"{gameObject.name}\" HAS NO COLLIDER/TRIGGER!");
        }
    }
    private void Awake()
    {
        useCollider.enabled = false;
    }
    private void Update()
    { 
        if (pickCooldown > 0)
        {
            pickCooldown -= Time.deltaTime;
            if (pickCooldown <= 0) 
            {
                pickCooldown = 0;
                useCollider.enabled = true;
            }
        }
    }

    public void Pick(int _amount)
    {
        amount -= _amount;

        if (amount <= 0)
        {
            Destroy(gameObject);
        }
    }
}
