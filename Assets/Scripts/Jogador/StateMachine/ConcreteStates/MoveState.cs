using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class MoveState : PlayerState
{
    public MoveState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void Enter()
    {
        player.animator.SetBool("Moving", true);
    }

    public override void FrameUpdate()
    {
        if (!player.isGrounded)
        {
            stateMachine.ChangeState(player.fallState);
            return;
        }

        if (player.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState(player.jumpState);
            return;
        }

        if (HandleInput() == 0)
        {
            stateMachine.ChangeState(player.idleState);
            return;
        }
    }


    public override void PhysicsUpdate()
    {
        this.FlipPlayer();
        float moveInput = HandleInput();
        player.rb.velocity = new Vector2(player.moveSpeed * moveInput,player.rb.velocity.y);
    }
}
