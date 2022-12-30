using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 2f;

    [Tooltip("Current speed [m/s]")]
    public float speed;
    [Tooltip("Current velocity")]
    public Vector2 velocity;
    [Tooltip("Current normalized movement input")]
    Vector2 moveInput;

    public delegate void OnBeginMove();
    public delegate void OnMove(Vector3 velocity);
    public delegate void OnEndMove();
    public event OnBeginMove onBeginMove;
    public event OnMove onMove;
    public event OnEndMove onEndMove;

    private void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();

        Vector2 deltaPos = moveInput * moveSpeed;
        float oldSpeed = this.speed;
        this.speed = deltaPos.magnitude;
        this.velocity = deltaPos;

        if (speed > 0f)
        {
            onMove?.Invoke(this.velocity);
            if (oldSpeed == 0)
            {
                onBeginMove?.Invoke();
            }
        }
        else
        {
            if (oldSpeed > 0)
            {
                onEndMove?.Invoke();
            }
        }

        transform.Translate(deltaPos * Time.deltaTime * Time.timeScale);
    }
}
