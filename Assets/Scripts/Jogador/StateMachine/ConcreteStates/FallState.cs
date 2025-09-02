using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallState : PlayerState
{
    public FallState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
    }
    public override void Enter()
    {
        player.animator.SetBool("Moving", true);
    }

    public override void FrameUpdate()
    {
        if (player.isGrounded)
        {
            if (HandleInput().Equals(0))
            {
                stateMachine.ChangeState(player.idleState);
                return;
            }
            else {
                stateMachine.ChangeState(player.moveState);
                return;
            }                
        }
    }

    public override void PhysicsUpdate()
    {
        this.FlipPlayer();
        float maxSpeed = player.moveSpeed;
        float targetX = HandleInput() * maxSpeed;
        float newVelX = Mathf.Lerp(player.rb.velocity.x, targetX, Time.fixedDeltaTime * 10f);

        player.rb.velocity = new Vector2(newVelX, player.rb.velocity.y);

        if (player.rb.velocity.y < 0f)            
        {
            float fallMultiplier = player.fallMult;  
            player.rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1f) * Time.fixedDeltaTime;
        }
    }

    public override void Exit()
    {
        player.animator.SetBool("Moving", false);
    }
}
