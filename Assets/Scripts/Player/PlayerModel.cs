using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerModel : PlayerComponent
{
    [SerializeField] Animator bodyAnimator;

    private void OnEnable()
    {
        player.movement.onBeginMove += () => { bodyAnimator.SetBool("isMoving", true); };
        player.movement.onEndMove += () => { bodyAnimator.SetBool("isMoving", false); };
    }
}
