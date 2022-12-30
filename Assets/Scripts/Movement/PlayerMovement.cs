using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerMovement : MonoBehaviour
{
    public float Speed = 2f;

    Vector2 movement;

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        movement.Normalize();

        transform.Translate(movement * Speed * Time.deltaTime * Time.timeScale, Space.World);
    }
}
