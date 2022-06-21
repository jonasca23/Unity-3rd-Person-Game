using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyImpactState : EnemyBaseState
{
    private readonly int ImpactAnimationHash = Animator.StringToHash("impact");
    private readonly float ImpactCrossfadeTime = 0.2f;

    float duration = 1f;

    public EnemyImpactState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(ImpactAnimationHash, ImpactCrossfadeTime);
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);
        duration -= deltaTime;

        if(duration <= 0f)
        {
            stateMachine.SwitchState(new EnemyIdleState(stateMachine));
        }
    }

    public override void Exit()
    {
    }
}
