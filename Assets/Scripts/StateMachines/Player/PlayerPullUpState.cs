using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPullUpState : PlayerBaseState
{
    private readonly int PullUpHash = Animator.StringToHash("PullUp");
    private readonly Vector3 Offset = new Vector3(0f, 2.325f, 0.65f);
    private const float CrossFadeTime = 0.1f;

    public PlayerPullUpState(PlayerStateMachine _stateMachine) : base(_stateMachine)
    {

    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(PullUpHash, CrossFadeTime);
    }
    
    public override void Tick(float deltaTime)
    {
        if (stateMachine.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f) return;

        stateMachine.Controller.enabled = false;
        stateMachine.transform.Translate(Offset, Space.Self);
        stateMachine.Controller.enabled = true;

        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine, false));
    }

    public override void Exit()
    {
        stateMachine.Controller.Move(Vector3.zero);
        stateMachine.ForceReceiver.Reset();
    }
}
