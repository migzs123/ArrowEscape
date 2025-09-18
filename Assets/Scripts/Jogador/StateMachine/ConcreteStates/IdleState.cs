using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class IdleState : PlayerState
{
    public IdleState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {}

    public override void Enter()
    {
        player.animator.SetBool("Moving", false);
    }

    public override void FrameUpdate()
    {
        if (!player.isGrounded)
        {
            stateMachine.ChangeState(player.fallState);
            return;
        }

        if ((player.coyoteTimeCounter > 0f && Input.GetKeyDown(KeyCode.Space)) ||
            (player.isGrounded && player.jumpBufferCounter > 0f))
        {
            stateMachine.ChangeState(player.jumpState);
            player.jumpBufferCounter = 0f; // reseta buffer
            return;
        }

        // Se tem input horizontal → Move
        if (HandleInput() != 0)
        {
            stateMachine.ChangeState(player.moveState);
            return;
        }
    }

    public override void PhysicsUpdate()
    {
        player.rb.velocity = new Vector2(0f, player.rb.velocity.y);
    }
}
