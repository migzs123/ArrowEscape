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
        // Se perdeu o chão → Fall (mesmo se velocidade ainda não for negativa)
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

        // Se tem input horizontal → Move
        if (HandleInput() != 0)
        {
            stateMachine.ChangeState(player.moveState);
            return;
        }
    }

    public override void PhysicsUpdate()
    {
        player.rb.velocity = new Vector2(0, player.rb.velocity.y);
    }
}
