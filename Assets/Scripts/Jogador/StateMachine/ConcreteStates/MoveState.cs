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

        // VERIFICAÇÃO SIMPLES DO PULO - DEIXE O BUFFER NO PLAYER
        if (player.jumpBufferCounter > 0f && player.coyoteTimeCounter > 0f)
        {
            stateMachine.ChangeState(player.jumpState);
            return;
        }

        // Transição para Idle
        if (HandleInput() == 0)
        {
            stateMachine.ChangeState(player.idleState);
            return;
        }
    }

    public override void PhysicsUpdate()
    {
        float moveInput = HandleInput();
        float targetSpeed = moveInput * player.moveSpeed;

        // Use Lerp para resposta mais rápida que MoveTowards
        float newX = Mathf.Lerp(player.rb.velocity.x, targetSpeed, player.accel * Time.fixedDeltaTime);

        player.rb.velocity = new Vector2(newX, player.rb.velocity.y);
    }
}
