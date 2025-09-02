using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandState : PlayerState
{
    public LandState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void Enter()
    {
        player.animator.SetTrigger("Land");
    }

    public override void FrameUpdate()
    {
        // Quando a animação terminar, troca para Idle ou Move
        if (player.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            if (Mathf.Abs(player.rb.velocity.x) > 0.1f)
                stateMachine.ChangeState(player.moveState);
            else
                stateMachine.ChangeState(player.idleState);
        }
    }
}
