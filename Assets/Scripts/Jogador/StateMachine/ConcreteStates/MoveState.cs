using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class MoveState : PlayerState
{
    public MoveState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine) { }

    public override void Enter()
    {
        player.animator.SetBool("Moving", true);
    }

    public override void FrameUpdate()
    {
        FlipPlayer();
        if (!player.isGrounded)
        {
            stateMachine.ChangeState(player.fallState);
            return;
        }

        if ((player.coyoteTimeCounter > 0f && Input.GetKeyDown(KeyCode.Space)) ||
            (player.isGrounded && player.jumpBufferCounter > 0f))
        {
            stateMachine.ChangeState(player.jumpState);
            player.jumpBufferCounter = 0f;
            return;
        }

        if (Mathf.Abs(player.rb.velocity.x) < 0.05f && HandleInput() == 0)
        {
            stateMachine.ChangeState(player.idleState);
            return;
        }
    }

    public override void PhysicsUpdate()
    {
        
        float moveInput = HandleInput();
        float targetSpeed = moveInput * player.moveSpeed;

        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? player.accel : player.decel;
        float newX = Mathf.MoveTowards(player.rb.velocity.x, targetSpeed, accelRate * Time.fixedDeltaTime);

        player.rb.velocity = new Vector2(newX, player.rb.velocity.y);
    }
}
