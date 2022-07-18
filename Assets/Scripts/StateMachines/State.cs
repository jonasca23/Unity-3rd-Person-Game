using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    public abstract void Enter();
    public abstract void Tick(float deltaTime);
    public abstract void Exit();

    protected float GetNormalizedTime(Animator _animator, string _tag)
    {
        AnimatorStateInfo currentInfo = _animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = _animator.GetNextAnimatorStateInfo(0);

        if (_animator.IsInTransition(0) && nextInfo.IsTag(_tag))
        {
            return nextInfo.normalizedTime;
        }
        else if (!_animator.IsInTransition(0) && currentInfo.IsTag(_tag))
        {
            return currentInfo.normalizedTime;
        }

        return 0;
    }
}
