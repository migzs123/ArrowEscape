using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : PlayerState
{
    public JumpState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
    }
    public override void Enter()
    {
        player.animator.SetTrigger("Jump");
        player.rb.velocity = new Vector2(player.rb.velocity.x, 0f); // Zera Y antes do impulso
        player.rb.AddForce(Vector2.up * player.jumpPower, ForceMode2D.Impulse);
    }

    public override void FrameUpdate()
    {
        if (player.rb.velocity.y < 0f)
        {
            stateMachine.ChangeState(player.fallState);
            return;
        }
    }

    public override void PhysicsUpdate()
    {
        if(this.HandleInput() != 0)
        {
            this.FlipPlayer();
        }
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

}
