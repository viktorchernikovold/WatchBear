using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float speed;
    public bool canMove = true;
    public Vector2 velocity;
    private Vector2 moveInput;
    public Rigidbody2D useRigidbody;

    public delegate void OnMove(Vector3 velocity);
    public event OnMove onMove;

    private void FixedUpdate()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();

        this.velocity = useRigidbody.velocity;
        this.speed = this.velocity.magnitude;
        if (this.speed > 0)
        {
            onMove?.Invoke(this.velocity);
        }
        if (canMove)
        {
            Vector2 deltaPos = moveInput * moveSpeed;
            useRigidbody.velocity = deltaPos;
        }
    }
}
