using UnityEngine;

public class JumpState : PlayerState
{
    public JumpState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine) { }

    public override void Enter()
    {
        player.animator.SetTrigger("Jump");

        // Zera Y para resetar impulso
        player.rb.velocity = new Vector2(player.rb.velocity.x, 0f);

        // Impulso inicial
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
        float moveInput = HandleInput();
        this.FlipPlayer();

        float airControlFactor = 1f;
        float targetX = moveInput * player.moveSpeed * airControlFactor;
        float newX = Mathf.Lerp(player.rb.velocity.x, targetX, player.airAccel * Time.fixedDeltaTime);
        player.rb.velocity = new Vector2(newX, player.rb.velocity.y);

        // --- Pulo variável (low jump) ---
        if (player.rb.velocity.y > 0f && !Input.GetButton("Jump"))
        {
            float lowJumpMultiplier = player.lowJumpMult; // ex: 2f
            player.rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1f) * Time.fixedDeltaTime;
        }
    }
}
