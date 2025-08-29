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
        if (!HandleInput().Equals(0) && player.isGrounded)
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
