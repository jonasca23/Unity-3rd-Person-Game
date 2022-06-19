using UnityEngine;

public class PlayerTargetingState : PlayerBaseState
{
    private readonly int TargetingBlendTreeHash = Animator.StringToHash("TargetingBlendTree");
    private readonly int TargetingForwardHash = Animator.StringToHash("TargetingForward");
    private readonly int TargetingRightHash = Animator.StringToHash("TargetingRight");

    public PlayerTargetingState(PlayerStateMachine _stateMachine) : base(_stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.InputReader.CancelEvent += OnCancel;
        stateMachine.Animator.Play(TargetingBlendTreeHash);
    }

    public override void Tick(float deltaTime)
    {
        if (stateMachine.InputReader.IsAttacking)
        {
            stateMachine.SwitchState(new PlayerAttackingState(stateMachine, 0));
            return;
        }

        if (stateMachine.Targeter.CurrentTarget == null)
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            return;
        }

        UpdateAnimator(deltaTime);
        FaceTarget();
        Move(CalculateMovement() * stateMachine.TargetingMovementSpeed, deltaTime);
    }

    public override void Exit()
    {
        stateMachine.InputReader.CancelEvent -= OnCancel;
    }

    private void OnCancel()
    {
        stateMachine.Targeter.Cancel();
        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    }

    private Vector3 CalculateMovement()
    {
        Vector3 movement = new Vector3();

        movement += stateMachine.transform.right * stateMachine.InputReader.MovementValue.x;
        movement += stateMachine.transform.forward * stateMachine.InputReader.MovementValue.y;
        movement.y = 0;

        return movement;
    }

    private void UpdateAnimator(float deltaTime)
    {
        stateMachine.Animator.SetFloat(TargetingForwardHash, stateMachine.InputReader.MovementValue.y, 0.1f, deltaTime);
        stateMachine.Animator.SetFloat(TargetingRightHash, stateMachine.InputReader.MovementValue.x, 0.1f, deltaTime);
    }
}
