using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerModel : PlayerComponent
{
    [SerializeField] Animator bodyAnimator;

    private void FixedUpdate()
    {
        bodyAnimator.SetBool("isMoving", player.movement.speed > 0 ? true : false);
    }
}
