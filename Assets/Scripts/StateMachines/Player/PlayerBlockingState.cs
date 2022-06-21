using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlockingState : PlayerBaseState
{
    private readonly int BlockHash = Animator.StringToHash("block");
    private const float CrossFadeDuration = 0.3f;

    public PlayerBlockingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(BlockHash, CrossFadeDuration);
        stateMachine.Health.SetInvunerable(true);
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);

        if (!stateMachine.InputReader.IsBlocking)
        {
            ReturnToLocomotion();
        }
    }

    public override void Exit()
    {
        stateMachine.Health.SetInvunerable(false);
    }
}
