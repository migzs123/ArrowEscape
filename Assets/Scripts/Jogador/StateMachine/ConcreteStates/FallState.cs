using UnityEngine;

public class FallState : PlayerState
{
    public FallState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine) { }

    public override void FrameUpdate()
    {
        FlipPlayer();
        if (player.isGrounded)
        {
            // Verifica se deve ir para Idle ou Move
            if (Mathf.Abs(HandleInput()) < 0.05f)
                stateMachine.ChangeState(player.idleState);
            else
                stateMachine.ChangeState(player.moveState);
            return;
        }
    }

    public override void PhysicsUpdate()
    {
        float moveInput = HandleInput();
        float airControlFactor = 1f;

        float targetSpeed = moveInput * player.moveSpeed * airControlFactor;
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? player.airAccel : player.airDecel;
        float newX = Mathf.Lerp(player.rb.velocity.x, targetSpeed, accelRate * Time.fixedDeltaTime);

        player.rb.velocity = new Vector2(newX, player.rb.velocity.y);

        if (player.rb.velocity.y < 0f) // caindo
        {
            float fallMultiplier = player.fallMult; // ex: 2.5f
            player.rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1f) * Time.fixedDeltaTime;
        }
        else if (player.rb.velocity.y > 0f && !Input.GetKey(KeyCode.Space)) // pulo "curto"
        {
            float lowJumpMultiplier = player.lowJumpMult; // ex: 2f
            player.rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1f) * Time.fixedDeltaTime;
        }
    }
}
