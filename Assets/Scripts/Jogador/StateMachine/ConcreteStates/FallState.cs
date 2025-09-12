using UnityEngine;

public class FallState : PlayerState
{
    public FallState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine) { }

    public override void FrameUpdate()
    {
        if (player.isGrounded)
        {
            if (HandleInput().Equals(0))
                stateMachine.ChangeState(player.idleState);
            else
                stateMachine.ChangeState(player.moveState);
        }
    }

    public override void PhysicsUpdate()
    {
        if (HandleInput() != 0) this.FlipPlayer();

        float maxSpeed = player.moveSpeed;
        float targetX = HandleInput() * maxSpeed;

        // Controle no ar pode ser reduzido em relação ao chão
        float airControl = 0.7f;
        float newVelX = Mathf.Lerp(player.rb.velocity.x, targetX, Time.fixedDeltaTime * (10f * airControl));

        player.rb.velocity = new Vector2(newVelX, player.rb.velocity.y);

        // Fall multiplier = acelera a queda
        if (player.rb.velocity.y < 0f)
        {
            float fallMultiplier = player.fallMult; // ex: 2.5f
            player.rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1f) * Time.fixedDeltaTime;
        }
    }
}
