using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float Speed = 2f;

    private void Update()
    {
        Vector2 movement = Vector2.zero;

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        movement.Normalize();

        transform.Translate(movement * Speed * Time.deltaTime * Time.timeScale);
    }
}
