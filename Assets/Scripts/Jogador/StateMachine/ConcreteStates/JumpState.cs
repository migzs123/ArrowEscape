using UnityEngine;

public class JumpState : PlayerState
{
    public JumpState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine) { }

    public override void Enter()
    {
        player.animator.SetTrigger("Jump");

        // Zera o Y para não acumular força
        player.rb.velocity = new Vector2(player.rb.velocity.x, 0f);

        // Impulso inicial
        player.rb.AddForce(Vector2.up * player.jumpPower, ForceMode2D.Impulse);
    }

    public override void FrameUpdate()
    {
        // Se começou a cair -> muda para FallState
        if (player.rb.velocity.y < 0f)
        {
            stateMachine.ChangeState(player.fallState);
            return;
        }

        // Pulo variável: corta altura se soltar o botão cedo
        if (player.rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            float cutMultiplier = player.lowJumpMult; // ex: 2f
            player.rb.velocity += Vector2.up * Physics2D.gravity.y * (cutMultiplier - 1f) * Time.deltaTime;
        }
    }

    public override void PhysicsUpdate()
    {
        if (HandleInput() != 0) this.FlipPlayer();

        float maxSpeed = player.moveSpeed;
        float targetX = HandleInput() * maxSpeed;
        float newVelX = Mathf.Lerp(player.rb.velocity.x, targetX, Time.fixedDeltaTime * 10f);

        player.rb.velocity = new Vector2(newVelX, player.rb.velocity.y);
    }
}
