using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerState
{
    protected Player player;
    protected PlayerStateMachine stateMachine;

    public PlayerState(Player player, PlayerStateMachine stateMachine)
    {
        this.player = player;
        this.stateMachine = stateMachine;
    }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void FrameUpdate() { }
    public virtual void PhysicsUpdate() { }
    //public virtual void AnimationTriggerEvent() { }

    public virtual float HandleInput()
    {
        return Input.GetAxisRaw("Horizontal");
    }

    public virtual void FlipPlayer()
    {
        player.transform.localScale = new Vector2(HandleInput() > 0 ? Mathf.Abs(player.transform.localScale.x) : -Mathf.Abs(player.transform.localScale.x), player.transform.localScale.y);
    }
}
