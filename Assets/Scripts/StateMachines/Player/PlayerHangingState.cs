using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHangingState : PlayerBaseState
{
    private readonly int HangingHash = Animator.StringToHash("HangingIdle");
    private const float CrossFadeTime = 0.1f;

    private Vector3 ledgeForward;

    public PlayerHangingState(PlayerStateMachine _stateMachine, Vector3 _ledgeForward) : base(_stateMachine)
    {
        ledgeForward = _ledgeForward;
    }

    public override void Enter()
    {
        stateMachine.transform.rotation = Quaternion.LookRotation(ledgeForward, Vector3.up);
        stateMachine.Animator.CrossFadeInFixedTime(HangingHash, CrossFadeTime);        
    }

    public override void Tick(float deltaTime)
    {
        if (stateMachine.InputReader.MovementValue.y < 0f)
        {
            OnDrop();
        }

        if(stateMachine.InputReader.MovementValue.y > 0f)
        {
            OnPullUp();
        }
    }

    public override void Exit()
    {
    }

    void OnDrop()
    {
        stateMachine.Controller.Move(Vector3.zero);
        stateMachine.ForceReceiver.Reset();
        stateMachine.SwitchState(new PlayerFallingState(stateMachine));
    }

    void OnPullUp()
    {
        stateMachine.SwitchState(new PlayerPullUpState(stateMachine));
    }
}