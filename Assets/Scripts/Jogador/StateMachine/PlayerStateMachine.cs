using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine 
{
    private PlayerState currState { get; set; }

    public void Start(PlayerState state) { 
        currState = state;
        currState.Enter();
    }

    public void ChangeState(PlayerState state) { 
        currState.Exit();
        currState = state;
        currState.Enter();
    }

    public void FrameUpdate() {
        currState.FrameUpdate();
    }

    public void PhysicsUpdate()
    {
        currState.PhysicsUpdate();
    }
}
