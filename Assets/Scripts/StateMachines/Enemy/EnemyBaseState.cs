using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseState : State
{
    protected EnemyStateMachine stateMachine;

    public EnemyBaseState(EnemyStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
    }

    protected bool IsInChaseRange()
    {
        if (stateMachine.PlayerHealth.IsDead)
        {
            return false;
        }

        float playerDistanceSqr = (stateMachine.PlayerHealth.transform.position - stateMachine.transform.position).sqrMagnitude;
        return playerDistanceSqr <= (stateMachine.PlayerChasingRange * stateMachine.PlayerChasingRange);
    }

    protected void Move(Vector3 motion, float deltaTime)
    {
        stateMachine.Controller.Move((motion + stateMachine.ForceReceiver.Movement) * deltaTime);
    }

    protected void Move(float deltaTime)
    {
        stateMachine.Controller.Move(stateMachine.ForceReceiver.Movement * deltaTime);
    }

    protected void FacePlayer()
    {
        if (stateMachine.PlayerHealth == null) return;

        Vector3 lookPos = stateMachine.PlayerHealth.transform.position - stateMachine.transform.position;
        lookPos.y = 0;

        stateMachine.transform.rotation = Quaternion.LookRotation(lookPos);
    }
}
