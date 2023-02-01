using UnityEngine;

public class HeroFreeLookState : HeroBaseState
{
    private readonly int FreeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");
    private readonly int FreeLookBlendTreeHash = Animator.StringToHash("FreeLookBlendTree");
    private const float AnimatorDampTime = 0.1f;
    private const float CrossFadeDuration = 0.1f;


    public HeroFreeLookState(HeroStateMachine stateMachine) : base(stateMachine) {}

    public override void Enter() 
    {
        stateMachine.InputReader.JumpEvent += OnJump;
        stateMachine.InputReader.DashEvent += OnDodge;

        stateMachine.Animator.SetFloat(FreeLookSpeedHash, 0f);
        stateMachine.Animator.CrossFadeInFixedTime(FreeLookBlendTreeHash, CrossFadeDuration);
    }

    public override void Tick(float deltaTime) 
    {
        Vector3 movement = CalculateMovement();

        Move(movement * stateMachine.FreeLookMovementSpeed, deltaTime);

        if (stateMachine.InputReader.MovementValue == Vector2.zero) {
            stateMachine.Animator.SetFloat(FreeLookSpeedHash, 0, AnimatorDampTime, deltaTime);
            return;
        }

        stateMachine.Animator.SetFloat(FreeLookSpeedHash, 1, AnimatorDampTime, deltaTime);
        FaceMovementDirection(movement, deltaTime);
    }

    public override void Exit() 
    {
        stateMachine.InputReader.JumpEvent -= OnJump;
        stateMachine.InputReader.DashEvent += OnDodge;
    }

    private void FaceMovementDirection(Vector3 movement, float deltaTime)
    {
        stateMachine.transform.rotation = Quaternion.Lerp(
            stateMachine.transform.rotation,
            Quaternion.LookRotation(movement),
            deltaTime * stateMachine.RotationDamping
        );
    }

    private void OnJump()
    {
        stateMachine.SwitchState(new HeroJumpingState(stateMachine));
    }

    private void OnDodge()
    {
        stateMachine.SwitchState(new HeroDashState(stateMachine));
    }
}
