using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackingState : PlayerBaseState
{
    private float previousFrameTime;
    private bool alreadyAppliedForce;

    private Attack attack;
    public PlayerAttackingState(PlayerStateMachine _stateMachine, int _attackIndex) : base(_stateMachine)
    {
        attack = stateMachine.Attacks[_attackIndex];
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(attack.AnimationName, attack.TransitionDuration);
        stateMachine.WeaponDamage.SetAttack(attack.WeaponDamage, attack.Knockback);
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);
        FaceTarget();

        float normalizedTime = GetNormalizedTime(stateMachine.Animator);
        if (normalizedTime >= previousFrameTime && normalizedTime < 1f)
        {
            if (normalizedTime >= attack.ForceTime)
            {
                TryApplyForce(attack.Force);
            }

            if (stateMachine.InputReader.IsAttacking)
            {
                TryComboAttack(normalizedTime);
            }
        }
        else
        {
            if(stateMachine.Targeter.CurrentTarget != null)
            {
                stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
            } else
            {
                stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            }
        }        

        previousFrameTime = normalizedTime;
    }

    public override void Exit()
    {
        
    }

    private void TryComboAttack(float normalizedTime)
    {
        if (attack.ComboStateIndex == -1) return;
        if (normalizedTime < attack.ComboAttackTime) return;

        stateMachine.SwitchState(new PlayerAttackingState(stateMachine, attack.ComboStateIndex));
    }

    private void TryApplyForce(float _force)
    {
        if (alreadyAppliedForce) return;

        stateMachine.ForceReceiver.AddForce(stateMachine.transform.forward * _force);
        alreadyAppliedForce = true;
    }
}