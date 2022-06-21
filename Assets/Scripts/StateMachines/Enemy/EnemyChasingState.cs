using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChasingState : EnemyBaseState
{
    private readonly int LocomotionHash = Animator.StringToHash("Locomotion");
    private readonly float LocomotionCrossFadeDuration = .2f;

    private readonly int SpeedHash = Animator.StringToHash("Speed");
    private readonly float AnimatorDampTime = .1f;

    public EnemyChasingState(EnemyStateMachine _stateMachine) : base(_stateMachine)
    {        
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(LocomotionHash, LocomotionCrossFadeDuration);
        stateMachine.Animator.SetFloat(SpeedHash, 0);
    }

    public override void Tick(float deltaTime)
    {
        if (!IsInChaseRange())
        {
            stateMachine.SwitchState(new EnemyIdleState(stateMachine));
            return;
        }

        if (IsInAttackRange())
        {
            stateMachine.SwitchState(new EnemyAttackingState(stateMachine));
            return;
        }

        MoveToPlayer(deltaTime);
        FacePlayer();

        stateMachine.Animator.SetFloat(SpeedHash, 1f, AnimatorDampTime, deltaTime);
    }

    public override void Exit()
    {
        stateMachine.Agent.ResetPath();
        stateMachine.Agent.velocity = Vector3.zero;
    }    

    void MoveToPlayer(float deltaTime)
    {
        if (stateMachine.Agent.isOnNavMesh)
        {
            stateMachine.Agent.destination = stateMachine.PlayerHealth.transform.position;
            Move(stateMachine.Agent.desiredVelocity.normalized * stateMachine.MovementSpeed, deltaTime);
        }
       
        stateMachine.Agent.velocity = stateMachine.Controller.velocity;
    }
    bool IsInAttackRange()
    {
        if(stateMachine.PlayerHealth.IsDead)
        {
            return false;
        }

        float playerDistanceSqr = (stateMachine.PlayerHealth.transform.position - stateMachine.transform.position).sqrMagnitude;
        return playerDistanceSqr <= stateMachine.AttackRange * stateMachine.AttackRange;
    }
}