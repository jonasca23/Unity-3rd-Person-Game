using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseState : State
{
    protected PlayerStateMachine stateMachine;

    public PlayerBaseState(PlayerStateMachine _stateMachine) 
    {
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        throw new System.NotImplementedException();
    }
    public override void Tick(float deltaTime)
    {
        throw new System.NotImplementedException();
    }

    public override void Exit()
    {
        throw new System.NotImplementedException();
    }    
}